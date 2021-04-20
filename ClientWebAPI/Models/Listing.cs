using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWebAPI.Models
{
    public class Listing
    {
        public string name { get; set; }
        public float pricePerPassenger { get; set; }
        public Vehicletype vehicleType { get; set; }
    }
}
