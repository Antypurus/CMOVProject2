using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherIO.Backend
{

    struct WeatherConditions
    {
        public float Temperature;
        public float Pressure;
        public float Humidity;
        public float wind_speed;
        public float wind_degree;
    }

    class WeatherAPI
    {
        private const string WEATHER_API_KEY = "584f4b630abaaa8f48b7c5ffb8102a27";

        public static WeatherConditions GetCityWeather(string city, string country)
        {
            string uri = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "," + country + "&units=metric&appid=" + WEATHER_API_KEY;
            JObject response = JObject.Parse(HTTPRequests.Get(uri));

            JObject main = (JObject)response.GetValue("main");
            JObject wind = (JObject)response.GetValue("wind");

            WeatherConditions weather = new WeatherConditions();
            weather.Temperature = (float)main.GetValue("temp");
            weather.Pressure = (float)main.GetValue("pressure");
            weather.Humidity = (float)main.GetValue("humidity");
            weather.wind_speed = (float)wind.GetValue("speed");
            weather.wind_degree = (float)wind.GetValue("deg");

            return weather;
        }

    }
}
