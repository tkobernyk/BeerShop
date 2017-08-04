using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using BeerShop.Api.OAuth2.UserManagers;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity.Owin;

namespace BeerShop.Api.OAuth2.Providers
{
    public class ClientCredentialsAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            if (!context.TryGetBasicCredentials(out string clientId, out string clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (null == context.ClientId || null == clientSecret || !Guid.TryParse(clientId, out Guid clientIdGuid))
            {
                context.SetError("invalid_credentials", "A valid client_Id and client_Secret must be provided");
                return Task.FromResult<object>(null);
            }

            // change to async
            var client = userManager.GetClient(clientIdGuid, clientSecret);

            if (client == null)
            {
                context.SetError("invalid_credentials", "A valid client_Id and client_Secret must be provided");
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(0);
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            Guid.TryParse(context.ClientId, out Guid clientId);

            var client = userManager.GetByClientId(clientId);

            if (client == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return Task.FromResult<object>(0);
            }

            var identity = new ClaimsIdentity(new GenericIdentity(context.ClientId, OAuthDefaults.AuthenticationType),
                                                context.Scope.Select(x => new Claim("urn:oauth:scope", x)));
            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "audience", context.ClientId ?? string.Empty
                }
            });
            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return Task.FromResult<object>(0);
        }
    }
}