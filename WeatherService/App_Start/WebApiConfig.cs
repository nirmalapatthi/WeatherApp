using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace WeatherApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
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
