using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Weather
    {
        [JsonProperty("coord")]
        public Location Location { get; set; }

        [JsonProperty("weather")]
        public List<BriefInfo> weather { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("main")]
        public MainInfo Main { get; set; }

        public int visibility { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("dt")]
        public int DewPoint { get; set; }

        [JsonProperty("sys")]
        public System sys { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public int cod { get; set; }
    }
}
