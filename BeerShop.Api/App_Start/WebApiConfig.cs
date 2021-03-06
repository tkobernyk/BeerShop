﻿using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Web.Http.Routing;
using Microsoft.Owin.Security.OAuth;

namespace BeerShop.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                }
            };

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/v{version:apiVersion}/{controller}/{id}",
                new { id = RouteParameter.Optional, version = "1" }
            );

            config.AddApiVersioning(o => o.AssumeDefaultVersionWhenUnspecified = true);
        }
    }
}
