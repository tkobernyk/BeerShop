using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BeerShop.Api.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private static readonly string[] _emptyArray = new string[0];
        private readonly string[] _rolesSplit = _emptyArray;
        private readonly string[] _usersSplit = _emptyArray;


        public override object TypeId { get; } = new object();

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException(nameof(actionContext));
            var principal = actionContext.ControllerContext.RequestContext.Principal;
            return principal?.Identity != null && principal.Identity.IsAuthenticated && 
                (_usersSplit.Length <= 0 || _usersSplit.Contains(principal.Identity.Name, StringComparer.OrdinalIgnoreCase)) && 
                (_rolesSplit.Length <= 0 || _rolesSplit.Any(principal.IsInRole));
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException(nameof(actionContext));
            if (SkipAuthorization(actionContext) || IsAuthorized(actionContext)) return;
            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException(nameof(actionContext));
            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, 
                "Request Not Authorized");
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}