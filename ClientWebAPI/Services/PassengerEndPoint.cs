using ClientWebAPI.Contracts;
using ClientWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebAPI.Services
{
    public class PassengerEndPoint: IPassengerEndPoint
    {
        private IAPIHelper _apiHelper;

        public PassengerEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;

        }
        public async Task<Passengers> GetPassengerList(int passNo)
        {
            string url = "";

            if (passNo > 0)
            {
                url = $"https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest"; 
            }
            else
            {
                throw new Exception("Please enter the corrext number");
            }

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
