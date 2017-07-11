using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    [RoutePrefix("api/Weather")]
    public class WeatherController : ApiController
    {
        private Data.Weather _weather;
        private Models.Weather _weatherInfo;

        public WeatherController()
        {
            _weather = new Data.Weather();
        }

        [HttpGet]
        public IHttpActionResult Get(string countryName, string cityName)
        {
            _weatherInfo = _weather.GetWeatherData(new Country { Name = countryName, City = new City { Name = cityName } });
            return Ok(_weatherInfo);
        }
    }
}
