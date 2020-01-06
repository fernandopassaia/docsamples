using System;
using Microsoft.Practices.Unity;
using BimManufact.WebApi.Models;
using BimManufact.WebApi.Resolver;

namespace BimManufact.WebApi
{
    public static class UnityConfiguration
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.RegisterType<IBimManufactWebApiContext, BimManufactWebApiContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IDummyUserResolver, DummyUserResolver>(new HierarchicalLifetimeManager());

            return container;
        });

        public static IUnityContainer Instance => Container.Value;
    }
}