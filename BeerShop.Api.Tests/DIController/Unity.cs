using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.DataStore.Infrastructure.Repository;
using BeerShop.Api.Tests.Stubs;
using Microsoft.Practices.Unity;
using BeerShop.DataStore.Models;

namespace BeerShop.Api.Tests.DIController
{
    class Unity
    {
        public static IUnityContainer Register()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IBeerShopContext, FakeBeerShopContext>();
            container.RegisterType<IRepository<Brewery>, BreweryRepository>();
            return container;
        }
    }
}
