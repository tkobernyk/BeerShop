using System.IdentityModel.Tokens;
using Owin;
using Microsoft.Owin;
using System.Web.Http;
using BeerShop.Api.Providers;
using IdentityServer3.AccessTokenValidation;
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
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions
            {
                Provider = new QueryStringOAuthBearerProvider("access_token")
            };
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }

        private static void ConfigureOAuthClientCredentials(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:61800/Token",
                RequiredScopes = new[] { "write" },
                ClientId = "62ecb8a5-87b5-497c-9522-0783a35b95b5",
                ClientSecret = "52ECF038E7BDAECC1C92A2D372E4C0ACB963E963",
                TokenProvider = new QueryStringOAuthBearerProvider("access_token")
            });
        }
    }
}
