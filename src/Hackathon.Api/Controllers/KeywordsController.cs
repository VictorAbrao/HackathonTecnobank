using System.Net;
using ErrorOr;
using Hackathon.AppService.Commands.Requests.Keywords;
using Hackathon.AppService.Commands.Responses.Keywords;
using Hackathon.AppService.Queries.Requests.Keywords;
using Hackathon.AppService.Queries.Responses.Concierge;
using Hackathon.AppService.Queries.Responses.Keywords;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeywordsController(ILogger<KeywordsController> logger, IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreateKeywordCommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<Error>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateKeywordCommandRequest request, CancellationToken ct)
        {
            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateKeywordCommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<Error>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateKeywordCommandRequest request, CancellationToken ct)
        {
            request.DefineId(id);

            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteByIdKeywordCommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<Error>), (int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(ReadByIdKeywordQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<Error>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReadByIdAsync(int id, CancellationToken ct)
        {
            var request = new ReadByIdKeywordQueryRequest();

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
        [ProducesResponseType(typeof(ReadKeywordsQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<Error>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReadAsync([FromQuery] ReadKeywordsQueryRequest request, CancellationToken ct)
        {
            var result = await mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
