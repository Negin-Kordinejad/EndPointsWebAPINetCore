using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPointsWebAPINetCore.Dtos
{
    public class JournyDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public List<ResultDto> Result { get; set; } = new List<ResultDto>();
    }

}
