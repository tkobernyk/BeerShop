using System;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using BeerShop.Api.OAuth2.Models;

namespace BeerShop.Api.OAuth2.UserManagers
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store) {}

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new BeerShopUserStore());
               //(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = true
                //RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public ApplicationUser GetClient(Guid clientId, string clientSecret)
        {
            var store = Store as BeerShopUserStore;
            var result = store?.GetUserByClientIdAndClientSecretAsync(clientId, clientSecret);
            return result?.Result;
        }

        public ApplicationUser GetByClientId(Guid clientId)
        {
            var store = Store as BeerShopUserStore;
            var result = store?.GetUserByClientIdAsync(clientId);
            return result?.Result;
        }
    }
}