using Domain.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Business.WeatherAPI
{
    public class OpenWeather
    {
        private const string WEATHER_API_KEY = "584f4b630abaaa8f48b7c5ffb8102a27";

        public static WeatherConditions GetCityWeather(string city, string country)
        {
            string uri = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "," + country + "&units=metric&appid=" + WEATHER_API_KEY;
            JObject response = JObject.Parse(HTTPRequests.GetAsync(uri).Result);

            JArray desc = (JArray)response.GetValue("weather");
            JObject main = (JObject)response.GetValue("main");
            JObject wind = (JObject)response.GetValue("wind");

            WeatherConditions weather = new WeatherConditions();

            foreach (JObject item in desc)
            {
                weather.Description = (string)item.GetValue("description");
            }

            weather.Temperature = (float)main.GetValue("temp");
            weather.MinTemp = (float)main.GetValue("temp_min");
            weather.MaxTemp = (float)main.GetValue("temp_max");
            weather.Pressure = (float)main.GetValue("pressure");
            weather.Humidity = (float)main.GetValue("humidity");
            weather.WindSpeed = (float)wind.GetValue("speed");
            weather.WindDegree = (float)wind.GetValue("deg");

            return weather;
        }

        public static List<WeatherForecast> GetCityWeatherForecast(string city, string country)
        {
            List<WeatherForecast> forecast_result = new List<WeatherForecast>();

            string uri = "https://api.openweathermap.org/data/2.5/forecast?q=" + city + "," + country + "&units=metric&appid=" + WEATHER_API_KEY;
            JObject response = JObject.Parse(HTTPRequests.GetAsync(uri).Result);
            JArray forecasts = (JArray)response.GetValue("list");

            foreach (JObject forecast in forecasts)
            {
                JArray desc = (JArray)forecast.GetValue("weather");
                JObject wind = (JObject)forecast.GetValue("wind");
                JObject main = (JObject)forecast.GetValue("main");

                WeatherForecast weatherForecast = new WeatherForecast();

                weatherForecast.Date = (string)forecast.GetValue("dt_txt");
                weatherForecast.MinTemp = (float)main.GetValue("temp_min");
                weatherForecast.MaxTemp = (float)main.GetValue("temp_max");
                weatherForecast.Humidity = (float)main.GetValue("humidity");
                weatherForecast.WindDegree = (float)wind.GetValue("deg");
                weatherForecast.WindSpeed = (float)wind.GetValue("speed");

                foreach (JObject item in desc)
                {
                    weatherForecast.Icon = (string)item.GetValue("icon");
                }

                forecast_result.Add(weatherForecast);
            }

            return forecast_result;
        }

    }
}
