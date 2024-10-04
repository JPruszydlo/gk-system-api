using gk_system_api.Models;
using gk_system_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gk_system_api.Controllers
{
    [Authorize()]
    [Route("[controller]")]
    public class ReferencesController : Controller
    {
        private readonly IDatabaseService _db;

        public ReferencesController(IDatabaseService db)
        {
            _db = db;
        }

        [HttpGet()]
        public IActionResult GetReferences([FromQuery] int? limit)
        {
            var references = _db.GetReferences(limit);
            if (references != null)
                return Ok(references);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReference([FromRoute] int id)
        {
            if (_db.DeleteReference(id))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost()]
        public IActionResult AddReference([FromBody] ReferenceViewModel reference)
        {
            if (_db.AddReference(reference))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
