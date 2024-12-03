using ErrorOr;
using Hackathon.AppService.Commands.Requests.Concierges;
using Hackathon.AppService.Commands.Responses.Concierges;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using MediatR;

namespace Hackathon.AppService.Handlers.Concierge
{
    public class UpdateConciergeStatusCommandRequestHandler(IConciergeService conciergeService, IUnitOfWork unitOfWork) : IRequestHandler<UpdateConciergeStatusCommandRequest, ErrorOr<UpdateConciergeStatusCommandResponse>>
    {
        public async Task<ErrorOr<UpdateConciergeStatusCommandResponse>> Handle(UpdateConciergeStatusCommandRequest request, CancellationToken ct)
        {
            var response = new UpdateConciergeStatusCommandResponse();

            try
            {
                await unitOfWork.OpenAsync(ct);

                var conciergeEntityResult = await conciergeService.ReadByIdAsync(request.Id, ct);

                if (conciergeEntityResult.IsError)
                    return conciergeEntityResult.Errors;

                if (conciergeEntityResult.Value is null)
                    return Error.NotFound("", "Concierge Not Found");

                conciergeEntityResult.Value.DefineStatus(request.Status);
                conciergeEntityResult.Value.DefineUpdateAt(DateTime.UtcNow);

                 await unitOfWork.BeginTransactionAsync(ct);

                await conciergeService.UpdateAsync(conciergeEntityResult.Value,ct);

                await unitOfWork.CommitAsync(ct);
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

            return response;
        }
    }
}
