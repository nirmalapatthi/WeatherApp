using System;
using System.Collections.Generic;
using WeatherApp.Models;
using GlobalWeatherServiceProxy.GlobalWeather;
using System.Xml;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;
using System.Configuration;

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
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(_globalWeather.GetCitiesByCountry(country.Name));
            var cities = new List<City>();
            foreach (XmlNode node in xmlDoc.SelectNodes("NewDataSet/Table/City"))
                cities.Add(new City() { Name = node.InnerText });
            return cities;
        }

        public Models.Weather GetWeatherData(Country country)
        {
            string xmlWeather = _globalWeather.GetWeather(country.Name, country.City.Name);
            // If weather data is not returned by global weather fall back on api.openweathermap.org
            // Note: During testing noticed global weather is returning 'Data not found' for any selected city
            if (xmlWeather == "Data Not Found")
                return GetOpenWeatherData(country.City.Name);
            return null;
        }


        private Models.Weather GetOpenWeatherData(string cityName)
        {
            var appid = ConfigurationManager.AppSettings["APPID"].ToString();
            var url = ConfigurationManager.AppSettings["OpenWeatherMapURL"].ToString();
            //var appid = "b3a2b3c5a6dabcf17117e877f1a1ebbb";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url + cityName + "&APPID=" + appid);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(new Uri(url + cityName + "&APPID=" + appid)).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Models.Weather>(responseData);
            }
            return null;
        }
    }
}
