using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiThrottle;

namespace PlanDigitization_api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                // Generic rate limit applied to ALL APIs
                Policy = new ThrottlePolicy(perSecond: 10, perMinute: 200, perHour: 2000)
                {
                    IpThrottling = true,
                    ClientThrottling = true,
                    EndpointThrottling = true,
                    EndpointRules = new Dictionary<string, RateLimits>
                    { 
             //Fine tune throttling per specific API here
                    { "*", new RateLimits { PerSecond = 10, PerMinute = 200, PerHour = 2000 } }
                    }
                },
                Repository = new CacheRepository()
            });



            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:62292", "*", "GET,POST");

            //EnableCorsAttribute cors = new EnableCorsAttribute("https://teali4metricstest.azurewebsites.net", "*", "GET,POST");



            //config.EnableCors(cors);
        }
    }
}
