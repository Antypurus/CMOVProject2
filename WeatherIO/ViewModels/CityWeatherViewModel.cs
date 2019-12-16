using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Weather.Application.Services;

namespace WeatherIO.ViewModels
{
    public class CityWeatherViewModel : BaseViewModel
    {
        private readonly WeatherService _weatherService;
        private readonly string _city;
        private readonly string _country;
        private readonly DateTime _today;
        private string _description;
        private string _temperature;
        private string _humidity;
        private string _windSpeed;
        private string _windDegree;
        private string _pressure;
        private string _minTemp;
        private string _maxTemp;
        private List<WeatherForecast> _forecasts;

        public CityWeatherViewModel(string city, string country)
        {
            _weatherService = new WeatherService();
            _city = city;
            _country = country;
            _today = DateTime.Today;
        }

        public string Country => _country;

        public string City => _city;

        public string CityCountry => City + ", " + Country;

        public DateTime Today => _today;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged();
            }
        }

        public string Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                NotifyPropertyChanged();
            }
        }

        public string Humidity
        {
            get => _humidity;
            set
            {
                _humidity = value;
                NotifyPropertyChanged();
            }
        }

        public string WindSpeed
        {
            get => _windSpeed;
            set
            {
                _windSpeed = value;
                NotifyPropertyChanged();
            }
        }

        public string WindDegree
        {
            get => _windDegree;
            set
            {
                _windDegree = value;
                NotifyPropertyChanged();
            }
        }

        public string Pressure
        {
            get => _pressure;
            set
            {
                _pressure = value;
                NotifyPropertyChanged();
            }
        }

        public string MinTemp
        {
            get => _minTemp;
            set
            {
                _minTemp = value;
                NotifyPropertyChanged();
            }
        }

        public string MaxTemp
        {
            get => _maxTemp;
            set
            {
                _maxTemp = value;
                NotifyPropertyChanged();
            }
        }

        public List<WeatherForecast> Forecasts
        {
            get => _forecasts;
            set
            {
                _forecasts = value;
                NotifyPropertyChanged();
            }
        }

        public void UpdateWeather()
        {
            var weather = _weatherService.GetCityWeather(City, Country);
            var weatherForecast = _weatherService.GetCityWeatherForecast(City, Country);

            Description = weather.Description.ToString();
            Temperature = weather.Temperature.ToString();
            Humidity = $"{weather.Humidity.ToString()} %";
            WindSpeed = $"{weather.WindSpeed.ToString()} m/s";
            WindDegree = $"{weather.WindDegree.ToString()} °";
            Pressure = $"{weather.Pressure.ToString()} hpa";
            MinTemp = $"{Math.Round(weather.MinTemp).ToString()}°";
            MaxTemp = $"{Math.Round(weather.MaxTemp).ToString()}°";

            List<WeatherForecast> forecastOneDay = new List<WeatherForecast>();
            List<WeatherForecast> forecastTwoDays = new List<WeatherForecast>();
            List<WeatherForecast> forecastThreeDays = new List<WeatherForecast>();
            List<WeatherForecast> forecastFourDays = new List<WeatherForecast>();
            List<WeatherForecast> forecastFiveDays = new List<WeatherForecast>();

            foreach (WeatherForecast forecast in weatherForecast)
            {
                var dt = DateTime.ParseExact(forecast.Date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                if ((Today.AddDays(1) - dt).TotalMinutes > 0)
                {
                    forecastOneDay.Add(forecast);
                }
                else if ((Today.AddDays(2) - dt).TotalMinutes > 0)
                {
                    forecastTwoDays.Add(forecast);
                }
                else if ((Today.AddDays(3) - dt).TotalMinutes > 0)
                {
                    forecastThreeDays.Add(forecast);
                }
                else if ((Today.AddDays(4) - dt).TotalMinutes > 0)
                {
                    forecastFourDays.Add(forecast);
                }
                else if ((Today.AddDays(5) - dt).TotalMinutes > 0)
                {
                    forecastFiveDays.Add(forecast);
                }
                else break;
            }

            float maxTempOneDay = forecastOneDay.Max(f => f.MaxTemp);
            float maxTempTwoDays = forecastTwoDays.Max(f => f.MaxTemp);
            float maxTempThreeDays = forecastThreeDays.Max(f => f.MaxTemp);
            float maxTempFourDays = forecastFourDays.Max(f => f.MaxTemp);
            float maxTempFiveDays = forecastFiveDays.Max(f => f.MaxTemp);

            float minTempOneDay = forecastOneDay.Min(f => f.MinTemp);
            float minTempTwoDays = forecastTwoDays.Min(f => f.MinTemp);
            float minTempThreeDays = forecastThreeDays.Min(f => f.MinTemp);
            float minTempFourDays = forecastFourDays.Min(f => f.MinTemp);
            float minTempFiveDays = forecastFiveDays.Min(f => f.MinTemp);

            var query = forecastOneDay.GroupBy(f => f.Icon)
                .Select(group => new { Location = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count);
        }
    }
}