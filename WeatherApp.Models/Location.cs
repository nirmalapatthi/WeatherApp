using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Location
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}
