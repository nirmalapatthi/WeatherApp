using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.Controllers;
using System.Web.Http;
using System.Web.Http.Results;

namespace WeatherApp.Tests.Controllers
{
    [TestClass]
    public class WeatherControllerTest
    {
        [TestMethod]
        public void TestGetWeather()
        {
            var controller = new WeatherController();

            IHttpActionResult result = controller.Get("Australia", "Sydney Airport");

            var response = result as OkNegotiatedContentResult<WeatherApp.Models.Weather>;

            Assert.IsNotNull(result);

            Assert.IsNotNull(response.Content.weather);
        }

        [TestMethod]
        public void TestGetWeather_Negative()
        {
            var controller = new WeatherController();

            IHttpActionResult result = controller.Get("rtrewtre","retrewtre");

            var response = result as OkNegotiatedContentResult<WeatherApp.Models.Weather>;

            Assert.IsNull(response.Content);

        }
    }
}
