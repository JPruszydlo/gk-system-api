using gk_system_api.Models;
using gk_system_api.Services.Interfaces;
using gk_system_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gk_system_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IDatabaseService _db;
        private readonly IVisitorService _visitorService;
        private readonly IAuthenticationService _authenticationService;

        public HomeController(
            IVisitorService visitorService,
            IDatabaseService db,
            IAuthenticationService authenticationService
        )
        {
            _visitorService = visitorService;
            _authenticationService = authenticationService;
            _db = db;
        }

        [HttpPost("authorizeclient"), FilterUrl, CheckApiKey]
        public IActionResult Authorize()
            => Ok(_authenticationService.AuthorizeClient(HttpContext.DecodeAuthHeader()));

        [HttpPost("login"), FilterUrl, FilterUser]
        public IActionResult Authenticate()
            => Ok(_authenticationService.AuthenticateUser(HttpContext.DecodeAuthHeader()));

        [HttpPost("set-password"), FilterUrl, FilterUser]
        public IActionResult SetNewPassword([FromQuery] string token)
        {
            if (_authenticationService.SetNewPassword(HttpContext.DecodeAuthHeader(), token))
                return Ok();
            return BadRequest();
        }

        [HttpGet("home-config"), Authorize()]
        public IActionResult GetHomePageConfig()
        {
            var config = _db.GetHomePageConfig();
            if (config == null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(config);
        }

        [HttpGet("generalconfig"), FilterUrl, Authorize()]
        public IActionResult GetGeneralConfig([FromQuery] int configGroup)
        {
            var config = _db.GetGeneralConfig((ConfigGroup)configGroup);
            if(config == null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(config);
        }
        [HttpPost("setgeneralconfig"), Authorize()]
        public IActionResult SetGeneralConfig([FromBody] GeneralConfigViewModel[] configs)
        {
            if (_db.SetGeneralConfig(configs))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpGet("visitors"), Authorize()]
        public IActionResult GetVisitors()
        {
            var topVisitors = _visitorService.GetVisitors();
            if (topVisitors == null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(topVisitors);
        }

        [HttpPost("checkvisitor"), Authorize()]
        public IActionResult CheckVisitor([FromBody] VisitorViewModel model)
        {
            _visitorService.CheckVisitor(model);
            return Ok();
        }
    }
}