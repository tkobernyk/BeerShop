using System.Web.Http;

namespace BeerShop.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(FormattersConfig.Register);
            GlobalConfiguration.Configure(UnityConfig.RegisterComponents);
            GlobalConfiguration.Configure(FiltersConfig.Register);
        }
    }
}
