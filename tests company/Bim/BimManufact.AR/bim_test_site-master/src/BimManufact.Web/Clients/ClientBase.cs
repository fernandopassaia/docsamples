using System;
using System.Configuration;
using System.Net.Http;

namespace BimManufact.Web.Clients
{
    public abstract class ClientBase
    {
        private readonly string _webApiConfigVariable = "webapiurl";

        protected HttpClient GetWebApiClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings[_webApiConfigVariable])
            };
        }
    }
}