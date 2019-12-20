using Domain.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weather.Application.Services;

namespace WeatherIO.ViewModels
{
    class ForecastInfoViewModel : BaseViewModel
    {
        private List<WeatherForecast> _forecast;
        private readonly WeatherService _weatherService;

        private string _title;
        public ForecastInfoViewModel(string city, string country)
        {
            _weatherService = new WeatherService();
            Title = "" + city + "," + country + " Forecast Graph";
            ForecastsIntervals = _weatherService.GetCityWeatherForecast(city, country);
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

        public List<WeatherForecast> ForecastsIntervals
        {
            get
            {
                foreach(WeatherForecast forecast in _forecast)
                {
                    forecast.Icon = "https://openweathermap.org/img/wn/" + forecast.Icon + "@2x.png";
                    if(forecast.Date.Split(null).Length > 1)
                        forecast.Date = forecast.Date.Split(null)[1];
                }
                return _forecast.Take(8).ToList();
            }
            set
            {
                _forecast = value;
            }
        }

    }
}
