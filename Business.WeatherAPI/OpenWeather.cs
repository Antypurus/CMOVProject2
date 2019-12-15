using Domain.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Business.WeatherAPI
{

    public struct WeatherForecast
    {
        public float min_temp;
        public float max_temp;
        public float wind_speed;
        public float wind_degree;
        public float Humidity;
    }

    public class OpenWeather
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
            weather.WindSpeed = (float)wind.GetValue("speed");
            weather.WindDegree = (float)wind.GetValue("deg");

            return weather;
        }

        public static List<WeatherForecast> GetCityWheatherForecast(string city, string country)
        {
            List<WeatherForecast> forecast_result = new List<WeatherForecast>();

            string uri = "https://api.openweathermap.org/data/2.5/forecast?q=" + city + "," + country + "&units=metric&appid=" + WEATHER_API_KEY;
            JObject response = JObject.Parse(HTTPRequests.Get(uri));
            JArray forecasts = (JArray)response.GetValue("list");

            foreach (JObject forecast in forecasts)
            {
                JObject wind = (JObject)forecast.GetValue("wind");
                JObject main = (JObject)forecast.GetValue("main");

                WeatherForecast weatherForecat = new WeatherForecast();
                weatherForecat.min_temp = (float)main.GetValue("temp_min");
                weatherForecat.max_temp = (float)main.GetValue("temp_max");
                weatherForecat.Humidity = (float)main.GetValue("humidity");
                weatherForecat.wind_degree = (float)wind.GetValue("deg");
                weatherForecat.wind_speed = (float)wind.GetValue("speed");

                forecast_result.Add(weatherForecat);
            }

            return forecast_result;
        }

    }
}
