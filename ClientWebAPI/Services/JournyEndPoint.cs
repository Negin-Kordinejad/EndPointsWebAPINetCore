using ClientWebAPI.Contracts;
using ClientWebAPI.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientWebAPI.Services
{
    public class JournyEndPoint : IJournyEndPoint
    {
        private IAPIHelper _apiHelper;

        public JournyEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;

        }
        public async Task<Journy> GetJournyList()
        {
            string url = "";

            url = $"https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest";

            using (HttpResponseMessage response = await _apiHelper.AppClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Journy passengers = await response.Content.ReadAsAsync<Journy>();

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
