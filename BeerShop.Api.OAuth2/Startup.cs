using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;


[assembly: OwinStartup(typeof(BeerShop.Api.OAuth2.Startup))]

namespace BeerShop.Api.OAuth2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
