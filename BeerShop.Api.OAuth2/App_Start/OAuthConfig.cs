using System;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using BeerShop.Api.OAuth2.Providers;

namespace BeerShop.Api.OAuth2.App_Start
{
    public class OAuthConfig
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public void ConfigureAuth(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new BeerShopOAuthAuthorizationServerProvider(),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}