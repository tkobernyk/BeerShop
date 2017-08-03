using System;
using BeerShop.Api.OAuth2.Providers;
using BeerShop.Api.OAuth2.UserManagers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace BeerShop.Api.OAuth2
{
    public class OAuthConfig
    {
        public static string PublicClientId { get; private set; }
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new BeerShopOAuthAuthorizationServerProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}