using Bim.WebUI.InterfaceApi;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Bim.WebUI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IManufacturerClient, ManufacturerClient>();
            container.RegisterType<IProductClient, ProductClient>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}