using ErrorOr;
using Hackathon.AppService.Commands.Requests.Keywords;
using Hackathon.AppService.Commands.Responses.Keywords;
using Hackathon.AppService.Mappers;
using Hackathon.AppService.Validators.Keywords;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using Hackathon.SharedKernel.Validations;
using MediatR;

namespace Hackathon.AppService.Handlers.Keywords
{
    public class CreateKeywordCommandRequestHandler(IKeywordsService keywordsService, IUnitOfWork unitOfWork) : IRequestHandler<CreateKeywordCommandRequest, ErrorOr<CreateKeywordCommandResponse>>
    {
        public async Task<ErrorOr<CreateKeywordCommandResponse>> Handle(CreateKeywordCommandRequest request, CancellationToken ct)
        {
			var response = new CreateKeywordCommandResponse();

            try
			{
				var validations = await new CreateKeywordCommandRequestValidator().ValidateAsync(request, ct);

				if (!validations.IsValid)
					return validations.Errors.ToValidation();

				var entity = KeywordsMapper.ToEntity(request);

                await unitOfWork.OpenAsync(ct);

                await unitOfWork.BeginTransactionAsync(ct);

				var keywordInsertResult = await keywordsService.InsertAsync(entity, ct);

				if (request.SubWords.Any())
					await InsertSubWords(request, keywordInsertResult.Value, ct);

                await unitOfWork.CommitAsync(ct);

                response.DefineId(keywordInsertResult.Value);

                return response;
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

		protected async Task InsertSubWords(CreateKeywordCommandRequest request, int wordParentId, CancellationToken ct) 
		{
			foreach (var subWord in request.SubWords)
			{
				var entity = KeywordsMapper.ToEntity(request);

				entity.DefineWordParentId(wordParentId);
				entity.DefineWord(subWord);
				entity.DefineSubWords(string.Empty);

                await keywordsService.InsertAsync(entity, ct);
			}
		}
    }
}
