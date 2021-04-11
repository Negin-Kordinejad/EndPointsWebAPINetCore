using ClientWebAPI.Model;
using System.Threading.Tasks;

namespace ClientWebAPI.Contracts
{
    public interface IPassengerEndPoint
    {
        Task<Passengers> GetPassengerList(int passNo);
    }
}
