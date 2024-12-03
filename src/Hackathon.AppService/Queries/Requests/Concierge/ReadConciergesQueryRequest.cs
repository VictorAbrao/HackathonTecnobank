using ErrorOr;
using Hackathon.AppService.Queries.Responses.Concierge;
using MediatR;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Queries.Requests.Concierge
{
    public class ReadConciergesQueryRequest : IRequest<ErrorOr<IList<ReadConciergesQueryResponse>>>
    {
        public Detrans Detran { get; set; }

        public void DefineDetran(Detrans detran) => Detran = detran;
    }
}
