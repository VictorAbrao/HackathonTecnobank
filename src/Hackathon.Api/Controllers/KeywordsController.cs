using Hackathon.AppService.Commands.Requests.Keywords;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeywordsController(ILogger<KeywordsController> logger, IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateKeywordCommandRequest request, CancellationToken ct)
        {
            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateKeywordCommandRequest request, CancellationToken ct)
        {
            request.DefineId(id);

            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct)
        {

            var request = new DeleteByIdKeywordCommandRequest();

            request.DefineId(id);

            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadByIdAsync(int id, CancellationToken ct)
        {
            var request = new ReadByIdKeywordCommandRequest();

            request.DefineId(id);

            var result = await mediator.Send(request, ct);

            if (result.IsError)
            {
                if (result.Errors.Where(w => w.Type is ErrorOr.ErrorType.NotFound).Any())
                    return NotFound(result.Errors.Where(w => w.Type is ErrorOr.ErrorType.NotFound).First());

                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet()]
        public async Task<IActionResult> ReadAsync(CancellationToken ct)
        {
            var request = new ReadKeywordsCommandRequest();

            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
