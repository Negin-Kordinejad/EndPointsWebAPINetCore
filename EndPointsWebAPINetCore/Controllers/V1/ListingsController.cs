using AutoMapper;
using ClientWebAPI.Contracts;
using EndPointsWebAPINetCore.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPointsWebAPINetCore.Controllers.V1
{
    [Produces("application/json")]
    [Route("Listings")]
    [ApiController]
  //  [Authorize]
    [ApiVersion("1.0")]
    public class ListingsController : ControllerBase
    {
        private readonly IJournyEndPoint _journyEndPoint;
        private readonly ILogger<ListingsController> _logger;
        private readonly IMapper _mapper;

        public ListingsController(ILogger<ListingsController> logger, IMapper mapper, IJournyEndPoint journyEndPoint)
        {
            _logger = logger;
            _mapper = mapper;
            _journyEndPoint = journyEndPoint;
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
        /// <response code="400">If the passNo is zero</response> 
        /// <response code="404">If the result is not found</response>     

        [HttpGet("{passNo:int}")]
        public async Task<ActionResult<JournyDto>> GetReport(int passNo)
        {
            if (passNo == 0)
            {
                return BadRequest("Please enter the corrext number");
            }

            var jList = await _journyEndPoint.GetJournyList();
        //    var List1 = jList.listings.GroupBy(l => l.vehicleType.name).Where(g => g.Key.ToUpper()== "Hatchback".ToUpper()).FirstOrDefault();
            var List = jList.listings.GroupBy(l => l.vehicleType.maxPassengers).Where(g => g.Key <= passNo).FirstOrDefault();
            if (List == null)
            {
                return NotFound();
            }
            var Tlist = List.Select(s =>
             new { Name = s.name, Total = s.pricePerPassenger * s.vehicleType.maxPassengers })
                    .OrderByDescending(o => o.Total).ToList();


            //  var Result = new { pList.from, pList.to, result = Tlist };
       //     var Rmap = _mapper.Map<List<ListingDto>>(Tlist);
            JournyDto Result = new()
            {
                From = jList.from,
                To = jList.to,
                Result = Tlist.ConvertAll(x => new ListingDto
                {
                    Name = x.Name,
                    Total = x.Total
                })
            };
            return Result;

        }


    }
}
