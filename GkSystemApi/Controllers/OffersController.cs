using gk_system_api.Models;
using gk_system_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gk_system_api.Controllers
{
    [Authorize()]
    [Route("[controller]")]
    public class OffersController : Controller
    {
        private readonly IDatabaseService _db;
        public OffersController(IDatabaseService db)
        {
            _db = db;
        }

        [HttpPut("state/{id}")]
        public IActionResult SetState([FromQuery] bool value, [FromRoute] int id)
        {
            if(_db.SetOfferState(value, id))
                return Ok();
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetOffer([FromRoute] int id)
        {
            var offer = _db.GetOffer(id);
            if (offer == null)
                return NotFound();
            return Ok(offer);
        }

        [HttpGet("thumbnails")]
        public IActionResult GetThumbnails()
        {
            var thumbnails = _db.GetOffersThumbnails();
            if (thumbnails == null)
                return NotFound();
            return Ok(thumbnails);
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var offers = _db.OffersMapped;
            if (offers == null)
                return NotFound();
            return Ok(offers);
        }

        [HttpPost()]
        public IActionResult AddOffer([FromBody] OfferViewModel offer)
        {
            if (_db.AddOffer(offer))
                return Ok(_db.OffersMapped);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOffer([FromRoute] int id, [FromBody] OfferViewModel offerModel)
        {
            if (_db.UpdateOffer(id, offerModel))
                return Ok(_db.OffersMapped);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOffer([FromRoute] int id)
        {
            if (_db.DeleteOffer(id))
                return Ok(_db.OffersMapped);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
