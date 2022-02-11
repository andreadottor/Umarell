namespace Dottor.Umarell.Server.Controllers
{
    using Dottor.Umarell.Shared;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BuildingSitesController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] IEnumerable<IFormFile> files)
        {
            var result = new ApiResult()
            {
                Result = false
            };



            return Ok(result);
        }
    }
}
