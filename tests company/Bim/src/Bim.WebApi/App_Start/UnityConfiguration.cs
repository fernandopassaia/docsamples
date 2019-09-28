using Bim.Repository.DataContext;
using Bim.Util.Resolver;
using System;
using Unity;
using Unity.Lifetime;

namespace Bim.WebApi.App_Start
{
    public static class UnityConfiguration
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.RegisterType<IBimContext, BimContext>(new HierarchicalLifetimeManager()); //i've moved to repositories, no Queries command on Controllers...
            container.RegisterType<IFakeUserResolver, FakeUserResolver>(new HierarchicalLifetimeManager());
            return container;
        });

        public static IUnityContainer Instance => Container.Value;
    }
}