using ErrorOr;
using Hackathon.AppService.Queries.Responses.Publications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Queries.Requests.Publications
{
    public class ReadPublicationsQueryRequest : IRequest<ErrorOr<ReadPublicationQueryResponse>>
    {
        public Detrans Detran { get; set; }
    }
}
