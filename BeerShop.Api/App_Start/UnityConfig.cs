using System.Web.Http;

using Unity.WebApi;
using Microsoft.Practices.Unity;

using BeerShop.Logging;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Models.v2;
using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.DataStore.Infrastructure.Repository;
using BeerShop.DataStore.Infrastructure.Repository.v2;


namespace BeerShop.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();
            container.RegisterInstance(GlobalLogger.DefaultLogger);
            container.RegisterType<IBeerShopContext, BeerShopContext>();
            container.RegisterType<Repository<Beer>, BeerRepository>();
            container.RegisterType<Repository<DraftBeer>, DraftBeerRepository>();
            container.RegisterType<Repository<Brewery>, BreweryRepository>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}