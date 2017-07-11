using System.Web.Http;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    [RoutePrefix("api/Weather")]
    public class WeatherController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string countryName, string cityName)
        {
            var weather = new Data.Weather();
            var weatherInfo = weather.GetWeatherData(new Country { Name = countryName, City = new City { Name = cityName } });
            return Ok(weatherInfo);
        }
    }
}
