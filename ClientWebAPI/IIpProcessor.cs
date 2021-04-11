using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebAPI
{
    public interface IIpProcessor
    {
        Task<Location> IpLocator(string ipAddress);
    }
}
