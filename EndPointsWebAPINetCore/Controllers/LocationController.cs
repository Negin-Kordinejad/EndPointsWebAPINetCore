using ClientWebAPI.Contracts;
using ClientWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace EndPointsWebAPINetCore.Controllers
{


    [Route("Location")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {

        private readonly IIpProcessor _ipProcessor;
        private readonly ILogger<LocationController> _logger;
        public LocationController(ILogger<LocationController> logger, IIpProcessor ipProcessor)
        {
            _logger = logger;
            _ipProcessor = ipProcessor;
        }

        /// <summary>
        /// Takes an IP address and returns the city location that corresponds to this IP.
        /// </summary>
        /// <returns>string</returns>
        /// <response code="200">Success with the result</response>
        /// <response code="400">If the Ip address not correct</response>     
        /// <response code="404">If the result not found</response>     
        [Produces("application/json")]
        [HttpGet("{ipAddress}", Name = "GetIpLocation")]

        public async Task<IActionResult> GetIpLocation(string ipAddress)
        {
            try
            {
                Location City = await _ipProcessor.IpLocator(ipAddress);
                if (City != null)
                {
                    return Ok(City.city);
                }

                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest($"Ip Address {ipAddress} is incorrect");
            }
        }
    }
}
