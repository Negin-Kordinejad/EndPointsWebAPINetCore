using ClientWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebAPI.Contracts
{
   public interface IPassengerEndPoint
    {
        Task<Passengers> GetPassengerList(int passNo);
    }
}
