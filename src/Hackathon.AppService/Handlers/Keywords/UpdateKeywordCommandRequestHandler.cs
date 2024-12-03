using ErrorOr;
using Hackathon.AppService.Commands.Requests.Keywords;
using Hackathon.AppService.Commands.Responses.Keywords;
using Hackathon.AppService.Mappers;
using Hackathon.AppService.Services;
using Hackathon.AppService.Validators.Keywords;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using Hackathon.SharedKernel.Validations;
using MediatR;

namespace Hackathon.AppService.Handlers.Keywords
{
    public class UpdateKeywordCommandRequestHandler(IKeywordsService keywordsService, IUnitOfWork unitOfWork) : IRequestHandler<UpdateKeywordCommandRequest, ErrorOr<UpdateKeywordCommandResponse>>
    {
        public async Task<ErrorOr<UpdateKeywordCommandResponse>> Handle(UpdateKeywordCommandRequest request, CancellationToken ct)
        {
            var response = new UpdateKeywordCommandResponse();

            try
            {
                var validations = await new UpdateKeywordCommandRequestValidator().ValidateAsync(request, ct);

                if (!validations.IsValid)
                    return validations.Errors.ToValidation();

                await unitOfWork.OpenAsync(ct);

                var readByIdResult = await keywordsService.ReadByIdAsync(request.Id, ct);

                if (readByIdResult.IsError)
                    return readByIdResult.Errors;

                if (readByIdResult.Value is null)
                    return Error.NotFound("", "Keyword Not Found");

                var entity = KeywordsMapper.ToEntity(request, readByIdResult.Value);


                await unitOfWork.BeginTransactionAsync(ct);

                await keywordsService.UpdateAsync(entity, ct);

                if (request.SubWords.Any())
                {
                    await keywordsService.DeleteAllByParentIdAsync(entity.Id, ct);
                    await InsertSubWords(request, entity, ct);
                }
                                   
                await unitOfWork.CommitAsync(ct);

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

        protected async Task InsertSubWords(UpdateKeywordCommandRequest request, KeywordEntity keywordEntity, CancellationToken ct)
        {
            foreach (var subWord in request.SubWords)
            {
                var entity = KeywordsMapper.ToEntity(request, keywordEntity);

                entity.DefineId(0);
                entity.DefineWordParentId(keywordEntity.Id);
                entity.DefineWord(subWord);

                await keywordsService.InsertAsync(entity, ct);
            }
        }
    }
}
