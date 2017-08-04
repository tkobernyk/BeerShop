using Owin;
using Microsoft.Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;



[assembly: OwinStartup(typeof(BeerShop.Api.Startup))]

namespace BeerShop.Api
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(FormattersConfig.Register);
            GlobalConfiguration.Configure(UnityConfig.RegisterComponents);
            GlobalConfiguration.Configure(FiltersConfig.Register);
            app.UseWebApi(GlobalConfiguration.Configuration);
        }

        private static void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}
