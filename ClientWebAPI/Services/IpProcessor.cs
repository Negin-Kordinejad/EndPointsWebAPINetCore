using ClientWebAPI.Contracts;
using ClientWebAPI.Models;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientWebAPI.Services
{
    public class IpProcessor : IIpProcessor
    {
        private IAPIHelper _apiHelper;

        public IpProcessor(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;

        }
        public async Task<Location> IpLocator(string ipAddress)
        {
            string url = "";

            if (IsValidateIP(ipAddress))
            {
                url = $"http://api.ipstack.com/{ ipAddress }" +
                    $"?access_key=6250bd112317e8e0044c75bdc8c838af" +
                    $"&fields=city";
            }
            else
            {
                throw new ArgumentException("Wrong IP", "ipAddress");
            }

            using (HttpResponseMessage response = await _apiHelper.AppClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Location location = await response.Content.ReadAsAsync<Location>();

                    return location;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private bool IsValidateIP(string Address)
        {

            // Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

            string Pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            Regex check = new Regex(Pattern);

            if (string.IsNullOrEmpty(Address))
            {
                return false;
            }
            else
            {
                return check.IsMatch(Address, 0);
            }
        }
    }
}