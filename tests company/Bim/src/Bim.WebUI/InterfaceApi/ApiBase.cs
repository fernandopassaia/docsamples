using System;
using System.Configuration;
using System.Net.Http;

namespace Bim.WebUI.InterfaceApi
{
    public abstract class ApiBase
    {
        private readonly string _webApiConfigVariable = "webapiurl"; //here will be the address of WebApi

        protected HttpClient GetWebApiClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings[_webApiConfigVariable])
            };
        }
    }
}