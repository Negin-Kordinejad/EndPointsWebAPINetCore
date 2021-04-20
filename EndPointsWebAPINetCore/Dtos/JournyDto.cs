using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPointsWebAPINetCore.Dtos
{
    /// <summary>
    /// This the Model for output 
    /// </summary>
    public class JournyDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public List<ListingDto> Result { get; set; } = new List<ListingDto>();
    }

}
