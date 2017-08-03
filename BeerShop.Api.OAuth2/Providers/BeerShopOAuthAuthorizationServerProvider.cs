using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Cookies;

using Microsoft.AspNet.Identity.Owin;

using BeerShop.Api.OAuth2.UserManagers;

namespace BeerShop.Api.OAuth2.Providers
{
    public class BeerShopOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public BeerShopOAuthAuthorizationServerProvider(string publicClientId)
        {
            if (string.IsNullOrEmpty(publicClientId))
                throw new ArgumentNullException(nameof(publicClientId));
            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
            var cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);

            var properties = CreateProperties(user.UserName);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
                context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId != _publicClientId) return Task.FromResult<object>(null);
            var expectedRootUri = new Uri(context.Request.Uri, "/");

            if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                context.Validated();
            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"userName", userName}
            };
            return new AuthenticationProperties(data);
        }
    }
}