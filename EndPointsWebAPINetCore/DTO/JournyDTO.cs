using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPointsWebAPINetCore.DTO
{
    public class JournyDTO
    {
        public string from { get; set; }
        public string to { get; set; }
        public List<ResultDTO> result { get; set; }
    }

    public class ResultDTO
    {
        public string name { get; set; }
        public float total { get; set; }
    }

}
