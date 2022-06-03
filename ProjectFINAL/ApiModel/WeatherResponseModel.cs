using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFINAL.ApiModel
{
    public class WeatherResponseModel
    {
        public WeatherLocationResponse location { get; set; }
        public WeatherCurrentResponse current { get; set; }

    }
    public class WeatherLocationResponse
    {

        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string tz_id { get; set; }
        public string localtime { get; set; }
    }
    public class WeatherCurrentResponse
    {
        public decimal temp_c { get; set; }
        public decimal temp_f { get; set; }
        public bool isDay { get; set; }
        public WeatherConditionResponse condition { get; set; }
        public decimal wind_mph { get; set; }
        public decimal wind_kph { get; set; }
        public decimal wind_degree { get; set; }
        public decimal humidity { get; set; }
        public decimal gust_mph { get; set; }
        public decimal gust_kph { get; set; }
        public decimal feelslike_c { get; set; }
    }
    public class WeatherConditionResponse
    {
        public string text { get; set; }
        public string icon { get; set; }
        public int code { get; set; }
    }
}