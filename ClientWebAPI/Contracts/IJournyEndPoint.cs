using ClientWebAPI.Models;
using System.Threading.Tasks;

namespace ClientWebAPI.Contracts
{
    public interface IJournyEndPoint
    {
        Task<Journy> GetJournyList();
    }
}
