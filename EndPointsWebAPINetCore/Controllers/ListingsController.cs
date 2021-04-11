﻿using ClientWebAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace EndPointsWebAPINetCore.Controllers
{
    [Produces("application/json")]
    [Route("Listings")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IPassengerEndPoint _passengerEndPoint;
        private readonly ILogger<ListingsController> _logger;
        public ListingsController(ILogger<ListingsController> logger, IPassengerEndPoint passengerEndPoint)
        {
            _logger = logger;
            _passengerEndPoint = passengerEndPoint;
        }
        /// <summary>
        ///  Takes the number of passengers as a parameter, calculate the total price and return the results sorted by total price
        /// </summary>
        /// /// <remarks>
        /// Sample response:
        ///     
        ///     {
        ///     "from": "Sydney Airport (SYD), T1 International Terminal",
        ///     "to":   "46 Church Street, Parramatta NSW, Australia",
        ///     
        ///        "result": [
        ///                   {
        ///                    "name": "Listing 4",
        ///                    "total": 286.95
        ///                   }
        ///                  ]
        ///      }
        /// </remarks>
        /// <param name="passNo"></param>
        /// <returns>Json</returns>
        /// <response code="200">Success with the result</response>
        /// <response code="404">If the result is not found</response>     

        [HttpGet("{passNo}")]
        public async Task<IActionResult> GetReport(int passNo)
        {
            var pList = await _passengerEndPoint.GetPassengerList(passNo);
            var List = pList.listings.GroupBy(l => l.vehicleType.maxPassengers).Where(g => g.Key == passNo).FirstOrDefault();

            if (List != null)
            {
                var Tlist = List.Select(s =>
                 new { Name = s.name, Total = s.pricePerPassenger * s.vehicleType.maxPassengers })
                        .OrderByDescending(o => o.Total).ToList();


                var Result = new { pList.from, pList.to, result = Tlist };

                return Ok(Result);
            }
            else
            {
                return NotFound();
            }

        }


    }
}
