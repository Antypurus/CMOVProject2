using Business.WeatherAPI;
using Domain.Models;

namespace Weather.Application.Services
{
    public class WeatherService
    {
        public WeatherConditions GetCityWeather(string city, string country) 
            => OpenWeather.GetCityWeather(city, country);
    }
}