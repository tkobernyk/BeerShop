using Owin;
using Microsoft.Owin;


[assembly: OwinStartup(typeof(BeerShop.Api.OAuth2.Startup))]

namespace BeerShop.Api.OAuth2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            OAuthConfig.ConfigureAuth(app);
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
