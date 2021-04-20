using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWebAPI.Models
{
   public  class Journy
    {
        public string from { get; set; }
        public string to { get; set; }
        public Listing[] listings { get; set; }
    }
}
