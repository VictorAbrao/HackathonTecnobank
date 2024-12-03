using ErrorOr;
using Hackathon.AppService.Commands.Requests.Keywords;
using Hackathon.AppService.Commands.Responses.Keywords;
using Hackathon.AppService.Validators.Keywords;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using Hackathon.SharedKernel.Validations;
using MediatR;

namespace Hackathon.AppService.Handlers.Keywords
{
    public class DeleteByIdKeywordCommandRequestHandler(IKeywordsService keywordsService, IUnitOfWork unitOfWork) : IRequestHandler<DeleteByIdKeywordCommandRequest, ErrorOr<DeleteByIdKeywordCommandResponse>>
    {
        public async Task<ErrorOr<DeleteByIdKeywordCommandResponse>> Handle(DeleteByIdKeywordCommandRequest request, CancellationToken ct)
        {
			try
			{
                var validations = await new DeleteByIdKeywordCommandRequestValidator().ValidateAsync(request, ct);

                if (!validations.IsValid)
                    return validations.Errors.ToValidation();

                await unitOfWork.OpenAsync(ct);

                var entity = await keywordsService.ReadByIdAsync(request.Id, ct);

                if (entity.IsError)
                    return entity.Errors;

                await unitOfWork.BeginTransactionAsync(ct);

                await keywordsService.DeleteAllByParentIdAsync(request.Id, ct);

                await keywordsService.DeleteAsync(request.Id, ct);

                await unitOfWork.CommitAsync(ct);

                return new();
			}
			catch (Exception)
			{
                await unitOfWork.RollbackAsync(ct);
                throw;
			}
			finally {
                await unitOfWork.CloseAsync(ct);
            }
        }
    }
}


