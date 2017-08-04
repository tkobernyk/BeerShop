using System;
using System.Security.Cryptography;
using System.Text;
using BeerShop.Api.OAuth2.Providers;
using BeerShop.Api.OAuth2.UserManagers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace BeerShop.Api.OAuth2
{
    public static class OAuthConfig
    {
        public static string PublicClientId => GetPublicClientId();
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new AuthorizationServerProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        private static string GetPublicClientId()
        {
            var input = Guid.NewGuid().ToString();
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (var b in hash)
                    sb.Append(b.ToString("X2"));
                return sb.ToString();
            }
        }
    }
}