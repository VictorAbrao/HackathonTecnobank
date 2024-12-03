using Hackathon.AppService.Commands.Requests.Concierges;
using Hackathon.AppService.Queries.Requests.Concierge;
using Hackathon.AppService.Queries.Requests.Publications;
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
        public async Task<IActionResult> GetAsync([FromQuery]int detran, CancellationToken ct)
        {
            var request = new ReadConciergesQueryRequest();

            request.DefineDetran((HackathonEnums.Detrans)detran);

            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("{id}/approve")]
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
