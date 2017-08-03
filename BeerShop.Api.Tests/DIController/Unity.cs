using Microsoft.Practices.Unity;

using BeerShop.DataStore.Models;
using BeerShop.DataStore.Models.v2;
using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.DataStore.Infrastructure.Repository;
using BeerShop.Api.Tests.Stubs.Infrastructure.Database;
using BeerShop.Api.Tests.Stubs.Infrastructure.Repository;

namespace BeerShop.Api.Tests.DIController
{
    internal static class Unity
    {
        public static IUnityContainer Register()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IBeerShopContext, FakeBeerShopContext>();
            container.RegisterType<Repository<DraftBeer>, FakeDraftBeerRepository>();
            container.RegisterType<Repository<Brewery>, FakeBreweryRepository>();
            return container;
        }
    }
}
