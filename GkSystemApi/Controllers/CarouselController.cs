using gk_system_api.Models;
using gk_system_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gk_system_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize()]
    public class CarouselController : Controller
    {
        private readonly IDatabaseService _db;

        public CarouselController(IDatabaseService db)
        {
            _db = db;
        }

        [HttpGet("config/{subPage}")]
        public IActionResult GetConfig([FromRoute] string subPage)
        {
            var config = _db.GetCarouselConfig(subPage);
            if (config != null)
                return Ok(config);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut("config")]
        public IActionResult UpdateConfig([FromBody] CarouselConfigViewModel[] configs)
        {
            if (_db.UpdateCarouselConfig(configs))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
