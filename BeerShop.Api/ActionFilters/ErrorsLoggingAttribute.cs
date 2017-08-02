using System.Linq;
using BeerShop.Logging;
using System.Web.Http.Filters;

namespace BeerShop.Api.ActionFilters
{
    public class ErrorsLoggingAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger _log;
        public ErrorsLoggingAttribute(ILogger logger)
        {
            _log = logger;
        }
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var logMessage = string.Format("Called {{'Controller': {0} 'Action': {1} 'Arguments': {2} }}", 
                            actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                            actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                            actionExecutedContext.ActionContext.ActionArguments.Select(x => x.Key + "=" + x.Value).ToArray());
            if (actionExecutedContext.Exception != null)
            {
                _log.Error("Executed with error " + logMessage);
                _log.Error(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);
            }
        }
    }
}