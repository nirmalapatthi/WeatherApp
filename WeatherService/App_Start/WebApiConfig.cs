using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WeatherService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            var enableCors = new EnableCorsAttribute("*", "*", "GET,POST");
            config.EnableCors(enableCors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
              name: "ApiByAction1",
              routeTemplate: "api/Country/CitiesByCountry/{countryName}",
              defaults: new { controller = "Country", action = "GetCitiesByCountry" }
            );

            config.Routes.MapHttpRoute(
             name: "ApiByAction2",
             routeTemplate: "api/Weather/{countryName}/{cityName}",
             defaults: new { controller = "Weather" }
           );
        }
    }
}
