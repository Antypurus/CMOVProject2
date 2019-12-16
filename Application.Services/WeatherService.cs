using Business.WeatherAPI;
using Domain.Models;
using System.Collections.Generic;

namespace Weather.Application.Services
{
    public class WeatherService
    {
        public WeatherConditions GetCityWeather(string city, string country) 
            => OpenWeather.GetCityWeather(city, country);

        public List<WeatherForecast> GetCityWeatherForecast(string city, string country)
            => OpenWeather.GetCityWeatherForecast(city, country);
    }
}