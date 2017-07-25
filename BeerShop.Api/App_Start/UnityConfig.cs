using Unity.WebApi;
using System.Web.Http;
using Microsoft.Practices.Unity;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.DataStore.Infrastructure.Repository;


namespace BeerShop.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();
            container.RegisterType<IBeerShopContext, BeerShopContext>();
            container.RegisterType<IBeerShopContext, BeerShopContext>();
            container.RegisterType<IRepository<Brewery>, BreweryRepository>();
            container.RegisterType<IRepository<Beer>, BeerRepository>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}