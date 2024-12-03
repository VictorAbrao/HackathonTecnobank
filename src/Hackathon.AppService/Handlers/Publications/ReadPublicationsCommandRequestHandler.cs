using ErrorOr;
using Hackathon.AppService.Commands.Requests.Publications;
using Hackathon.AppService.Commands.Responses.Publications;
using Hackathon.AppService.Validators;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Adapters;
using Hackathon.SharedKernel.Adapters.Responses;
using Hackathon.SharedKernel.Data;
using Hackathon.SharedKernel.Factories;
using Hackathon.SharedKernel.Validations;
using MediatR;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Handlers.Publications
{
    public class ReadPublicationsCommandRequestHandler(
        IKeywordsService keywordsService,
        IUnitOfWork unitOfWork,
        IPublicationsService publicationsService,
        IDetranAdapterFactory detranAdapterFactory,
        IAdapterIA adapterIA)
        : IRequestHandler<ReadPublicationsCommandRequest, ErrorOr<ReadPublicationCommandResponse>>
    {
        private List<string> Keywords = new();

        public async Task<ErrorOr<ReadPublicationCommandResponse>> Handle(ReadPublicationsCommandRequest request, CancellationToken ct)
        {
            PublicationsEntity? publicationsEntity = null;

            try
            {
                var validations = await new ReadPublicationsCommandRequestValidator()
                    .ValidateAsync(request, ct);

                if (!validations.IsValid)
                    return validations.Errors.ToValidation();

                await unitOfWork.OpenAsync(ct);

                await ReadAllKeywords(request.Detran, ct);

                var getPublicationByDetranResult = await publicationsService.GetByDetranAsync(request.Detran, ct);

                if (getPublicationByDetranResult.IsError)
                    return getPublicationByDetranResult.Errors;
                else
                    publicationsEntity = getPublicationByDetranResult.Value;

                var lastReadPublicationDate = DefineLastRun(publicationsEntity);

                if (getPublicationByDetranResult.Value is null)
                    publicationsEntity = await InsertPublicationLastReadAsync(new PublicationsEntity() { LastReadPublications = lastReadPublicationDate, Detran = request.Detran }, ct);

                var detranInstance = detranAdapterFactory.GetAdapterInstance(request.Detran);

                var detranReadPublicationsResult = await detranInstance.ReadPublications(new() { LastReadPublicationDate = lastReadPublicationDate });

                if (detranReadPublicationsResult.IsError)
                    return detranReadPublicationsResult.Errors;

                if (detranReadPublicationsResult.Value is null)
                    return new();

                if (detranReadPublicationsResult.Value.Publications.Any())
                {
                    var publicationsAnalyzed = AnalyzePublications(detranReadPublicationsResult.Value.Publications, ct);

                    await InsertUpdatePublicationsAsync(publicationsAnalyzed, ct);
                }

                await UpdatePublicationLastReadAsync(publicationsEntity, ct);

                return new();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync(ct);
                throw;
            }
            finally
            {
                await unitOfWork.CloseAsync(ct);
            }
        }

        protected async Task ReadAllKeywords(Detrans detran, CancellationToken ct)
        {
            var result = await keywordsService.ReadByDetranAsync((int)detran, ct);

            if(!result.IsError)
                Keywords = result.Value.Select(s => s.Word).ToList();
        }

        protected DateTime DefineLastRun(PublicationsEntity publicationsEntity)
            => publicationsEntity is null ? DateTime.UtcNow.AddDays(-1) : publicationsEntity.LastReadPublications!.Value.AddDays(-1);

        protected async Task IndexPublicationAtAdapterIAAsync(IList<ReadDetranPublicationResponse> publications, CancellationToken ct)
        {
            await adapterIA.IndexAsync(publications, ct);
        }

        protected async Task<PublicationsEntity> InsertPublicationLastReadAsync(PublicationsEntity publicationsEntity, CancellationToken ct)
        {
            await unitOfWork.BeginTransactionAsync(ct);

            await publicationsService.InsertAsync(publicationsEntity, ct);

            await unitOfWork.CommitAsync(ct);

            return publicationsEntity;
        }

        protected async Task UpdatePublicationLastReadAsync(PublicationsEntity publicationsEntity, CancellationToken ct)
        {
            await unitOfWork.BeginTransactionAsync(ct);

            publicationsEntity.LastReadPublications = DateTime.UtcNow;

            await publicationsService.UpdateAsync(publicationsEntity, ct);

            await unitOfWork.CommitAsync(ct);
        }

        protected IList<ReadDetranPublicationResponse> AnalyzePublications(IList<ReadDetranPublicationResponse> publications, CancellationToken ct)
        {
            var publicationToIndex = new List<ReadDetranPublicationResponse>();

            foreach (var publication in publications)
            {
                var content = publication.Content;

                foreach (var keyword in Keywords)
                {
                    if (content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        publicationToIndex.Add(publication);
                        break;
                    }
                }
            }

            return publicationToIndex;
        }

        protected async Task InsertUpdatePublicationsAsync(IList<ReadDetranPublicationResponse> readDetranPublicationResponses,CancellationToken ct) { }
    }
}

