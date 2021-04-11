using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using EndPointsWebAPINetCore.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointsWebAPINetCore.Controllers
{
    [Route("candidate")]
    [Produces("application/json")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ILogger<Candidate> _logger;

        public CandidateController(ILogger<Candidate> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Provides test information
        /// </summary>
        /// <returns>Jason</returns>
        [HttpGet]
        public Candidate Get()
        {
            return new Candidate() { name = "test", phone = "test" };
        }

    }
}
