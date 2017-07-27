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
            _log.Info("Called {'Controller':" + filterContext.ControllerContext.ControllerDescriptor.ControllerName +
                    " 'Action':" + filterContext.ActionDescriptor.ActionName +
                    " 'Arguments':" + string.Join(";", filterContext.ActionArguments.Select(x => x.Key + "=" + x.Value).ToArray()) +
                    "}");
        }
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            var logMessage = "{'Controller':" + filterContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName +
                    " 'Action':" + filterContext.ActionContext.ActionDescriptor.ActionName +
                    " 'Arguments':" + string.Join(";", filterContext.ActionContext.ActionArguments.Select(x => x.Key + "=" + x.Value).ToArray()) +
                    "}";
            _log.Info("Executed " + logMessage);
            if (filterContext.Exception != null)
            {
                _log.Error("Executed with error " + logMessage);
                _log.Error(filterContext.Exception.Message, filterContext.Exception);
            }
        }
    }
}