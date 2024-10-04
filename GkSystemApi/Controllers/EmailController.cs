using gk_system_api.Models;
using gk_system_api.Services.Interfaces;
using gk_system_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gk_system_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : Controller
    {
        private readonly IDatabaseService _db;
        private readonly IEmailService _emailService;

        public EmailController(IDatabaseService db, IEmailService emailService)
        {
            _db = db;
            _emailService = emailService;
        }

        [HttpGet(), Authorize()]
        public IActionResult GetEmails()
        {
            var emails = _db.GetEmails();
            if(emails == null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(emails);
        }

        [HttpDelete(), Authorize()]
        public IActionResult RemoveEmails([FromQuery] string ids)
        {
            var emails = _db.RemoveEmails(ids);
            if(emails == null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(emails);
        }

        [HttpPost("send"), FilterUser]
        public IActionResult SendPortfolioEmail([FromBody] Contact contact)
        {
            if (_emailService.SendPortfolioEmail(contact))
                return Ok();
            return BadRequest();
        }


        [HttpPost("mail"), Authorize()]
        public IActionResult SendGkSystemEmails([FromBody] FullEmailViewModel fullModel)
        {
            if (_emailService.SendGkSystemEmails(fullModel))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("reset-pass-request"), FilterUrl]
        public IActionResult SendResetLink([FromBody] ResetPassRequestModel resetModel)
        {
            if (_emailService.SendGkSystemResetLink(resetModel))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}