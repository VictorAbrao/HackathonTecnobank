using Hackathon.AppService.Queries.Requests.Publications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicationsController(ILogger<PublicationsController> logger, IMediator mediator) : ControllerBase
    {                    
        [HttpPost]
        public async Task<IActionResult> RunAsync([FromBody] ReadPublicationsQueryRequest request, CancellationToken ct)
        {
            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
