using Microsoft.Practices.Unity;
using Unity.WebApi;
using BeerShop.DataStore;
using System.Web.Http;

namespace BeerShop.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();
            container.RegisterType<IBeerShopContext, BeerShopContext>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}