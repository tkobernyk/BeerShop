using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BeerShop.Logging;

namespace BeerShop.Api.ActionFilters
{
    public class LoggingAttribute : ActionFilterAttribute
    {
        private readonly ILogger _log;
        public LoggingAttribute(ILogger logger)
        {
            _log = logger;
        }
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            _log.Info(string.Format("Called {{'Controller': {0} 'Action': {1} 'Arguments': {2} }}",
                filterContext.ControllerContext.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName,
                filterContext.ActionArguments.Select(x => x.Key + "=" + x.Value).ToArray()));
        }
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            _log.Info(string.Format("Executed {{'Controller': {0} 'Action': {1} 'Arguments': {2} }}",
                filterContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName, 
                filterContext.ActionContext.ActionDescriptor.ActionName,
                filterContext.ActionContext.ActionArguments.Select(x => x.Key + "=" + x.Value).ToArray()));
        }
    }
}