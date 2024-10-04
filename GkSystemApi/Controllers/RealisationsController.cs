using gk_system_api.Models;
using gk_system_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gk_system_api.Controllers
{
    [Authorize()]
    [Route("[controller]")]
    public class RealisationsController : Controller
    {
        private readonly IDatabaseService _db;
        public RealisationsController(IDatabaseService db)
        {
            _db = db;
        }

        [HttpPut("favourites/{imageId}")]
        public IActionResult SetFavourite([FromRoute] int imageId, [FromQuery] bool value)
        {
            if(_db.SetFavouriteRealisation(imageId, value))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("favourites")]
        public IActionResult GetFavourites()
        {
            var favourites = _db.GetFavouriteRealisations();
            if (favourites != null)
                return Ok(favourites);
            return NotFound();
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var realisations = _db.RealisationsMapped;
            if (realisations == null)
                return NotFound();
            return Ok(realisations);
        }

        [HttpDelete("image/{id}")]
        public IActionResult DeleteRealisationImage([FromRoute] int id)
        {
            if(_db.DeleteRealisation(id))
                return Ok(_db.RealisationsMapped);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRealisation([FromRoute] int id)
        {
            if (_db.DeleteRealisation(id))
                return Ok(_db.RealisationsMapped);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost()]
        public IActionResult AddRealisation([FromBody] RealisationViewModel realisation)
        {
            if (_db.AddRealisation(realisation))
                return Ok(_db.RealisationsMapped);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
