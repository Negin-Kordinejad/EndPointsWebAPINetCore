using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWebAPI.Model
{
   public  class Passengers
    {
        public string from { get; set; }
        public string to { get; set; }
        public Listing[] listings { get; set; }
    }
}
