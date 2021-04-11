using ClientWebAPI.Contracts;
using ClientWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointsWebAPINetCore.Controllers
{
    [Produces("application/json")]
    [Route("Listings")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private IPassengerEndPoint _passengerEndPoint;

        public ListingsController(IPassengerEndPoint passengerEndPoint)
        {
            _passengerEndPoint = passengerEndPoint;
        }
        /// <summary>
        ///  EndPoint takes the number of passengers as a parameter, calculate the total price and return the results sorted by total price
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///     "from": "Sydney Airport (SYD), T1 International Terminal",
        ///     "to": "46 Church Street, Parramatta NSW, Australia",
        ///        "result": [{
        ///        "name": "Listing 4",
        ///       "total": 286.95
        ///        "isComplete": true
        ///                   }
        ///                 ]
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


                var Result = new { from = pList.from, to = pList.to, result = Tlist };

                return Ok(Result);
            }
            else
            {
                return NotFound();
            }

        }


    }
}
