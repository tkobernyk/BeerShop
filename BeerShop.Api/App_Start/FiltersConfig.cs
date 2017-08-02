﻿using System.Web.Http;
using BeerShop.Logging;
using BeerShop.Api.ActionFilters;

namespace BeerShop.Api
{
    public class FiltersConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var logger = config.DependencyResolver.GetService(typeof(ILogger)) as ILogger;
            config.Filters.Add(new LoggingAttribute(logger));
            config.Filters.Add(new ErrorsLoggingAttribute(logger));
            config.Filters.Add(new ValidateAttribute());
        }
    }
}