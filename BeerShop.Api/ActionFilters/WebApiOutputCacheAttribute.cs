using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BeerShop.Api.ActionFilters
{
    public class WebApiOutputCacheAttribute : ActionFilterAttribute
    {
        private readonly int _timespan;
        private readonly int _clientTimeSpan;
        private readonly bool _anonymousOnly;
        private string _cachekey;
        private static readonly ObjectCache WebApiCache = MemoryCache.Default;

        public WebApiOutputCacheAttribute(int timespan, int clientTimeSpan, bool anonymousOnly)
        {
            _timespan = timespan;
            _clientTimeSpan = clientTimeSpan;
            _anonymousOnly = anonymousOnly;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!_isCacheable(actionContext)) return;
            var mediaTypeWithQualityHeaderValue = actionContext.Request.Headers.Accept.FirstOrDefault();
            if (mediaTypeWithQualityHeaderValue != null)
                _cachekey = string.Join(":", actionContext.Request.RequestUri.AbsolutePath, 
                    mediaTypeWithQualityHeaderValue.ToString());
            if (!WebApiCache.Contains(_cachekey)) return;
            var cache = WebApiCache.Get(_cachekey) as string;
            if (string.IsNullOrEmpty(cache)) return;
            actionContext.Response = actionContext.Request.CreateResponse();
            actionContext.Response.Content = new StringContent(cache);
            var contenttype = WebApiCache.Get(_cachekey + ":response-ct") as MediaTypeHeaderValue ?? 
                              new MediaTypeHeaderValue(_cachekey.Split(':')[1]);
            actionContext.Response.Content.Headers.ContentType = contenttype;
            actionContext.Response.Headers.CacheControl = SetClientCache();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (!WebApiCache.Contains(_cachekey))
            {
                var body = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
                WebApiCache.Add(_cachekey, body, DateTime.Now.AddSeconds(_timespan));
                WebApiCache.Add(_cachekey + ":response-ct", actionExecutedContext.Response.Content.Headers.ContentType, 
                    DateTime.Now.AddSeconds(_timespan));
            }
            if (_isCacheable(actionExecutedContext.ActionContext))
                actionExecutedContext.ActionContext.Response.Headers.CacheControl = SetClientCache();
        }

        private bool _isCacheable(HttpActionContext actionContext)
        {
            return !(_timespan == 0 || _clientTimeSpan == 0) &&
                   !(_anonymousOnly && Thread.CurrentPrincipal.Identity.IsAuthenticated) &&
                   actionContext.Request.Method == HttpMethod.Get;
        }

        private CacheControlHeaderValue SetClientCache()
        {
            return new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(_clientTimeSpan),
                MustRevalidate = true
            }; 
        }
    }
}