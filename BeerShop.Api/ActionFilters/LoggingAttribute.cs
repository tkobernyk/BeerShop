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
            _log.Info(
                $"Called {{'Controller': {filterContext.ControllerContext.ControllerDescriptor.ControllerName} " + 
                $"'Action': {filterContext.ActionDescriptor.ActionName} " + 
                $"'Arguments': {filterContext.ActionArguments.Select(x => x.Key + "=" + x.Value).ToArray()} }}");
        }
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            _log.Info(
                $"Executed {{'Controller': {filterContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName} " + 
                $"'Action': {filterContext.ActionContext.ActionDescriptor.ActionName} " + 
                $"'Arguments': {filterContext.ActionContext.ActionArguments.Select(x => x.Key + "=" + x.Value).ToArray()} }}");
        }
    }
}