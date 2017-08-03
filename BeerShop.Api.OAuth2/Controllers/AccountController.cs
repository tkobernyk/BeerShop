using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BeerShop.Api.OAuth2.Models;
using BeerShop.Api.OAuth2.Models.AccountBinding;
using BeerShop.Api.OAuth2.Providers;
using BeerShop.Api.OAuth2.Results;
using BeerShop.Api.OAuth2.UserManagers;
using BeerShop.Api.OAuth2.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace BeerShop.Api.OAuth2.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string _localLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController() {}

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            return new UserInfoViewModel
            {
                Login = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin?.LoginProvider
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
                return null;

            var logins = new List<UserLoginInfoViewModel>();

            /*foreach (var linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = _localLoginProvider,
                    ProviderKey = user.UserName,
                });
            }*/

            return new ManageInfoViewModel
            {
                LocalLoginProvider = _localLoginProvider,
                Login = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePassword model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            return !result.Succeeded ? GetErrorResult(result) : Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPassword model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
            return !result.Succeeded ? GetErrorResult(result) : Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLogin model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);
            if (ticket?.Identity == null || ticket.Properties?.ExpiresUtc != null && 
                ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow)
                return BadRequest("External login failure.");
            var externalData = ExternalLoginData.FromIdentity(ticket.Identity);
            if (externalData == null)
                return BadRequest("The external login is already associated with an account.");
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            return !result.Succeeded ? GetErrorResult(result) : Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLogin model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            IdentityResult result;
            if (model.LoginProvider == _localLoginProvider)
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            else
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));

            return !result.Succeeded ? GetErrorResult(result) : Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (!string.IsNullOrEmpty(error))
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));

            if (!User.Identity.IsAuthenticated)
                return new ChallengeResult(provider, this);

            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            if (externalLogin == null)
                return InternalServerError();
            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }
            var user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));
            if (user != null)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                var oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                var cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                var properties = BeerShopOAuthAuthorizationServerProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                var claims = externalLogin.GetClaims();
                var identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            var descriptions = Authentication.GetExternalAuthenticationTypes();
            string state = null;
            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }

            return descriptions.Select(description => new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = OAuthConfig.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state
                    }),
                    State = state
                })
                .ToList();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(Register model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = new ApplicationUser { Id = Guid.NewGuid().ToString(),  UserName = model.Login };
            var result = await UserManager.CreateAsync(user, model.Password);
            return !result.Succeeded ? GetErrorResult(result) : Ok();
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternal model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
                return InternalServerError();
            var user = new ApplicationUser { UserName = model.Email };
            var result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            return !result.Succeeded ? GetErrorResult(result) : Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                UserManager.Dispose();
            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication => Request.GetOwinContext().Authentication;

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return InternalServerError();

            if (result.Succeeded) return null;
            if (result.Errors != null)
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error);
            if (ModelState.IsValid)
                return BadRequest();
            return BadRequest(ModelState);
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IEnumerable<Claim> GetClaims()
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider)
                };
                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }
                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                var providerKeyClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(providerKeyClaim?.Issuer) || string.IsNullOrEmpty(providerKeyClaim.Value) 
                    || providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                    return null;
                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static readonly RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", nameof(strengthInBits));
                }

                var strengthInBytes = strengthInBits / bitsPerByte;

                var data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
