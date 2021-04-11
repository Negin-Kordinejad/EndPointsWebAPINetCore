﻿using ClientWebAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EndPointsWebAPINetCore.Controllers
{

    [Produces("application/json")]
    [Route("Location")]
    [ApiController]

    public class LocationController : ControllerBase
    {

        private IIpProcessor _ipProcessor;
        public LocationController(IIpProcessor ipProcessor)
        {
            _ipProcessor = ipProcessor;
        }

        /// <summary>
        /// Takes an IP address and returns the city location that corresponds to this IP.
        /// </summary>
        /// <returns>string</returns>
        /// <response code="200">Success with the result</response>
        /// <response code="400">If the Ip address not correct</response>     
        /// <response code="404">If the result not found</response>     

        [HttpGet("{ipAddress}")]
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