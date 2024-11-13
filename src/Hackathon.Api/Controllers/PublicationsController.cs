using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicationsController(ILogger<PublicationsController> _logger) : ControllerBase
    {
                    
        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken ct)
        {
            return Ok("");
        }
    }
}
