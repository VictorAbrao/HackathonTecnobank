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
    public class ReadConciergesQueryRequestHandler(IConciergeService conciergeService, IUnitOfWork unitOfWork) : IRequestHandler<ReadConciergesQueryRequest, ErrorOr<ReadConciergesQueryResponse>>
    {
        public async Task<ErrorOr<ReadConciergesQueryResponse>> Handle(ReadConciergesQueryRequest request, CancellationToken ct)
        {
            var response = new ReadConciergesQueryResponse();

            try
			{
                var readConciergesRequestDTO = ConciergeMapper.ToDto(request);

                await unitOfWork.OpenAsync(ct);

                var readConciergesResponseDTO = await conciergeService.ReadAsync(readConciergesRequestDTO, ct);

                if (readConciergesResponseDTO.IsError)
                    return readConciergesResponseDTO.Errors;

                response = ConciergeMapper.ToResponse(readConciergesRequestDTO, readConciergesResponseDTO.Value);
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
