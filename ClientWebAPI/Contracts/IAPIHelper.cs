using System.Net.Http;

namespace ClientWebAPI.Contracts
{
    public interface IAPIHelper
    {
        HttpClient AppClient { get; }
    }
}
