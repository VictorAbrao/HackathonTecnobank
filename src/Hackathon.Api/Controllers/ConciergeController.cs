using Hackathon.AppService.Commands.Requests.Publications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConciergeController(ILogger<ConciergeController> logger, IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetAsync([FromBody] ReadPublicationsCommandRequest request, CancellationToken ct)
        {
            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
