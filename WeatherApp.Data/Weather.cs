using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using GlobalWeatherServiceProxy.GlobalWeather;
using System.Xml.Linq;
using System.Xml;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WeatherApp.Data
{
    public class Weather
    {
        private GlobalWeatherSoapClient _globalWeather;

        public Weather()
        {
            _globalWeather = new GlobalWeatherSoapClient();
        }

        public IEnumerable<City> GetCitiesByCountry(Country country)
        {
            IList<City> cities;
            string xml = _globalWeather.GetCitiesByCountry(country.Name);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            cities = new List<City>();
            foreach(XmlNode node in xmlDoc.SelectNodes("NewDataSet/Table/City"))
            {
                cities.Add(new City() { Name = node.InnerText });
            }

            return cities;
        }

        public Models.Weather GetWeatherData(Country country)
        {
            string xml = _globalWeather.GetWeather(country.Name, country.City.Name);

            Models.Weather weatherData = null;
            if (xml == "Data Not Found")
            {
                // If weather data is not returned by global weather fall back on api.openweathermap.org
                weatherData = GetOpenWeatherData(country.City.Name);
            } else
            {
                // weatherData = parse the xml however, Global weather is not displaying weather for any country/city so,
                // not sure about the xml response structure
            }
            return weatherData;
        }


        private Models.Weather GetOpenWeatherData(string cityName)
        {
            var appid = "b3a2b3c5a6dabcf17117e877f1a1ebbb";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&APPID=" + appid);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(new Uri("http://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&APPID=" + appid)).Result;

            Models.Weather weatherInfo = null;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                weatherInfo = JsonConvert.DeserializeObject<Models.Weather>(responseData);
            }

            return weatherInfo;
        }
    }
}
 