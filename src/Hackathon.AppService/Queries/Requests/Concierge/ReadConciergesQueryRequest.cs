using ErrorOr;
using Hackathon.AppService.Queries.Responses.Concierge;
using MediatR;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Queries.Requests.Concierge
{
    public class ReadConciergesQueryRequest : IRequest<ErrorOr<ReadConciergesQueryResponse>>
    {
        public int? UF { get; set; }
        public int? Status { get; set; }
        public string? Title { get; set; }
        public string? FileName { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 25;
    }
}
