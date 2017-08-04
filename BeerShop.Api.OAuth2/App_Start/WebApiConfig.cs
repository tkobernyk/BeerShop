using System.Web.Http;

namespace BeerShop.Api.OAuth2
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
            return config;
        }
    }
}
