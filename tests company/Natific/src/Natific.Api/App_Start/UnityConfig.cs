using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity;
using WebGrease;
using Natific.Infra.Contexts;
using Natific.Infra.Repositories;
using Natific.Domain.Repositories;
using Natific.Domain.Command.Handlers;
using System;

namespace Natific.Api
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<NatificContext, NatificContext>(new PerRequestLifetimeManager());//, If i want to inject the Connection String...
                //new InjectionConstructor("name=NatificConnection")); //here i inject the connection string (in Web.config) to the DbContext class
            container.RegisterType<IProductRepository, ProductRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IStockPileRepository, StockPileRepository>(new PerRequestLifetimeManager());
            container.RegisterType<ProductHandler, ProductHandler>(new PerRequestLifetimeManager());
            container.RegisterType<StockPileHandler, StockPileHandler>(new PerRequestLifetimeManager());
        }
    }
}