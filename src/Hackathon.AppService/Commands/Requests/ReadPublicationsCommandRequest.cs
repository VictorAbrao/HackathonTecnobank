﻿using ErrorOr;
using Hackathon.AppService.Commands.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Commands.Requests
{
    public class ReadPublicationsCommandRequest : IRequest<ErrorOr<ReadPublicationCommandResponse>>
    {
        public Detrans Detran { get; set; }
    }
}
