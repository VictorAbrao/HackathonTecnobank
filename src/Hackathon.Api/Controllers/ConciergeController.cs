using System.Net;
using ErrorOr;
using Hackathon.AppService.Commands.Requests.Concierges;
using Hackathon.AppService.Commands.Responses.Concierges;
using Hackathon.AppService.Queries.Requests.Concierge;
using Hackathon.AppService.Queries.Responses.Concierge;
using Hackathon.SharedKernel.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConciergeController(ILogger<ConciergeController> logger, IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ReadConciergesQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<Error>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] ReadConciergesQueryRequest  request, CancellationToken ct)
        {
            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("{id}/approve")]
        [ProducesResponseType(typeof(UpdateConciergeStatusCommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<Error>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateStatusToApprovedAsync(int id, CancellationToken ct)
        {
            var request = new UpdateConciergeStatusCommandRequest();
            request.DefineId(id);
            request.DefineStatus(HackathonEnums.ConciergeStatus.Approved);

            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("{id}/deny")]
        [ProducesResponseType(typeof(UpdateConciergeStatusCommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<Error>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateStatusToDeniedAsync(int id, CancellationToken ct)
        {
            var request = new UpdateConciergeStatusCommandRequest();
            request.DefineId(id);
            request.DefineStatus(HackathonEnums.ConciergeStatus.Denied);

            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
