using ErrorOr;
using Hackathon.AppService.Commands.Responses.Concierges;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Commands.Requests.Concierges
{
    public class UpdateConciergeStatusCommandRequest : IRequest<ErrorOr<UpdateConciergeStatusCommandResponse>>
    {
        public int Id { get; set; }
        public ConciergeStatus Status { get; set; }
        public void DefineId(int id) => Id = id;
        public void DefineStatus(ConciergeStatus status) => Status = status;
    }
}
