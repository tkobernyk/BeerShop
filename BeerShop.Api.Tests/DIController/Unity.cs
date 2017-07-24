using BeerShop.DataStore;
using BeerShop.Api.Tests.Stubs;
using Microsoft.Practices.Unity;


namespace BeerShop.Api.Tests.DIController
{
    class Unity
    {
        public static IUnityContainer Register()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IBeerShopContext, FakeBeerShopContext>();
            return container;
        }
    }
}
