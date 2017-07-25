using Microsoft.Practices.Unity;

using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.DataStore.Infrastructure.Repository;
using BeerShop.Api.Tests.Stubs.Infrastructure.Database;
using BeerShop.Api.Tests.Stubs.Infrastructure.Repository;

namespace BeerShop.Api.Tests.DIController
{
    class Unity
    {
        public static IUnityContainer Register()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IBeerShopContext, FakeBeerShopContext>();
            container.RegisterType<IRepository<Beer>, FakeBeerRepository>();
            container.RegisterType<IRepository<Brewery>, FakeBreweryRepository>();
            return container;
        }
    }
}
