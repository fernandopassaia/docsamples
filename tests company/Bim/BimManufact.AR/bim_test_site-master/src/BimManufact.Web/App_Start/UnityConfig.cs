using BimManufact.Web.Clients;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace BimManufact.Web
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