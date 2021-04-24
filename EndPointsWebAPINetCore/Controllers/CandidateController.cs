using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace EndPointsWebAPINetCore.Controllers
{
    [Authorize]
    [Route("candidate")]
  //  [Produces("application/json")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ILogger<CandidateController> _logger;

        public CandidateController(ILogger<CandidateController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Provides test information
        /// </summary>
        /// <returns>Jason</returns>
       
        [HttpGet]
        public IActionResult Get() => Ok(new { name = "test", phone = "test" });

    }
}
