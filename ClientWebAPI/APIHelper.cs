using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ClientWebAPI
{
    public class APIHelper:IAPIHelper
    {
        public  HttpClient AppClient { get; set; }
        public APIHelper()
        {
            InitializeClient();
        }
        public  void InitializeClient()
        {
            AppClient = new HttpClient();
            AppClient.DefaultRequestHeaders.Accept.Clear();
            AppClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


    }
}
