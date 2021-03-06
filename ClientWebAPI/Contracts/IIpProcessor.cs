using ClientWebAPI.Models;
using System.Threading.Tasks;

namespace ClientWebAPI.Contracts
{
    public interface IIpProcessor
    {
        Task<Location> IpLocator(string ipAddress);
    }
}
