using ClientWebAPI.Contracts;
using ClientWebAPI.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientWebAPI.Services
{
    public class PassengerEndPoint : IPassengerEndPoint
    {
        private IAPIHelper _apiHelper;

        public PassengerEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;

        }
        public async Task<Passengers> GetPassengerList()
        {
            string url = "";

            url = $"https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest";

            using (HttpResponseMessage response = await _apiHelper.AppClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Passengers passengers = await response.Content.ReadAsAsync<Passengers>();

                    return passengers;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
