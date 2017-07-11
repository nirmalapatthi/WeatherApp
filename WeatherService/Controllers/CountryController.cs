using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeatherApp.Models;
using WeatherApp.Data;
using System.Web.Http.Cors;

namespace WeatherApp.Controllers
{
    [RoutePrefix("api/Country")]
    public class CountryController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetCitiesByCountry(string countryName)
        {
            var weather = new Data.Weather();
            var cityList = weather.GetCitiesByCountry(new Country { Name = countryName });
            return Ok(cityList);
        }
        
    }
}
