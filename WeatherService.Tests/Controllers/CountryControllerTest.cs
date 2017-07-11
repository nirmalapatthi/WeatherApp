using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections;
using System.Collections.Generic;

namespace WeatherApp.Tests.Controllers
{
    [TestClass]
    public class CountryControllerTest
    {
        [TestMethod]
        public void TestGetCitiesByCountry()
        {
            CountryController controller = new CountryController();

            IHttpActionResult result = controller.GetCitiesByCountry("Australia");
            
            var response = result as OkNegotiatedContentResult<IEnumerable<WeatherApp.Models.City>>;

            Assert.IsNotNull(result);

            var cities = (List<WeatherApp.Models.City>) response.Content;

            Assert.IsTrue(cities.Count > 0);
        }

        [TestMethod]
        public void TestGetCitiesByCountry_Negative()
        {
            CountryController controller = new CountryController();

            IHttpActionResult result = controller.GetCitiesByCountry("xyz");

            var response = result as OkNegotiatedContentResult<IEnumerable<WeatherApp.Models.City>>;

            Assert.IsNotNull(result);

            var cities = (List<WeatherApp.Models.City>)response.Content;

            Assert.IsTrue(cities.Count == 0);
        }
    }
}
