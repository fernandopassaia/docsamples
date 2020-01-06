using Bim.WebApi.App_Start;
using Bim.WebApi.DependencyResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Bim.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.DependencyResolver = new UnityDependencyResolver(UnityConfiguration.Instance);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
