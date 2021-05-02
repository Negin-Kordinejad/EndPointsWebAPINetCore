using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace EndPointsWebAPINetCore.Controllers.V1
{
  //  [Authorize]
    [Route("candidate")]
  //  [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
  
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
       
        [HttpGet,MapToApiVersion("1.0")]
        public IActionResult Get() => Ok(new { name = "test", phone = "test" });

     
    }
}
