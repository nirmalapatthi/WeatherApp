using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    /// <summary>
    /// Weather Brief Information. It is part of the weather JSON data structure returned by the service
    /// </summary>
    public class BriefInfo
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}
