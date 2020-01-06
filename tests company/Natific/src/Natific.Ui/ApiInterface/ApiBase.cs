using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Natific.Ui.ApiInterface
{
    public abstract class ApiBase
    {
        private readonly string _webApiConfigVariable = "webapiurl"; //here will be the address of WebApi

        protected HttpClient GetWebApiUrl()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings[_webApiConfigVariable])
            };
        }
    }
}