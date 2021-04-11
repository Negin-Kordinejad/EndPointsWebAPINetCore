using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ClientWebAPI
{
    public interface IAPIHelper
    {
        HttpClient AppClient { get; }
    }
}
