using ErrorOr;
using Hackathon.AppService.Mappers;
using Hackathon.AppService.Queries.Requests.Concierge;
using Hackathon.AppService.Queries.Responses.Concierge;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.AppService.Handlers.Concierge
{
    public class ReadConciergesQueryRequestHandler(IConciergeService conciergeService, IUnitOfWork unitOfWork) : IRequestHandler<ReadConciergesQueryRequest, ErrorOr<IList<ReadConciergesQueryResponse>>>
    {
        public async Task<ErrorOr<IList<ReadConciergesQueryResponse>>> Handle(ReadConciergesQueryRequest request, CancellationToken ct)
        {
            var response = new List<ReadConciergesQueryResponse>();

            try
			{
                await unitOfWork.OpenAsync(ct);

                var conciergeEntities = await conciergeService.ReadAsync(request.Detran, ct);

                if (conciergeEntities.IsError)
                    return conciergeEntities.Errors;

                response = ConciergeMapper.ToResponse(conciergeEntities.Value);                
            }
			catch (Exception ex)
			{
                Error.Failure("", ex.Message);
			}
            finally 
            {
                await unitOfWork.CloseAsync(ct);
            }

            return response;
        }
    }
}
