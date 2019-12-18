using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Weather.Application.Services;

namespace WeatherIO.ViewModels
{
    public class CityWeatherViewModel : BaseViewModel
    {
        private readonly WeatherService _weatherService;
        private string _description;
        private string _temperature;
        private string _humidity;
        private string _windSpeed;
        private string _windDegree;
        private string _pressure;
        private string _minTemp;
        private string _maxTemp;
        private string _icon;
        private List<WeatherForecast> _forecasts;
        private bool _favorited;
        private bool _notFavorited;

        public CityWeatherViewModel(string city, string country)
        {
            _weatherService = new WeatherService();
            City = city;
            Country = country;
            Today = DateTime.Today;
            CheckFavorite();
        }

        public string Country { get; }

        public string City { get; }

        public string CityCountry => City + ", " + Country;

        public DateTime Today { get; }

        public string TodayDate => Today.ToString("D");

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

        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
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

        public bool Favorited
        {
            get => _favorited;
            set
            {
                _favorited = value;
                NotifyPropertyChanged();
            }
        }

        public bool NotFavorited
        {
            get => _notFavorited;
            set
            {
                _notFavorited = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<FavoriteCity> FavoritesList { set; get; }

        public void UpdateWeather()
        {
            var weather = _weatherService.GetCityWeather(City, Country);
            var weatherForecast = _weatherService.GetCityWeatherForecast(City, Country);
            //_forecasts = weatherForecast;

            Description = weather.Description.ToString();
            Temperature = weather.Temperature.ToString();
            Humidity = $"{weather.Humidity.ToString()} %";
            WindSpeed = $"{weather.WindSpeed.ToString()} m/s";
            WindDegree = $"{weather.WindDegree.ToString()} °";
            Pressure = $"{weather.Pressure.ToString()} hpa";
            MinTemp = $"{weather.MinTemp.ToString()}°";
            MaxTemp = $"{weather.MaxTemp.ToString()}°";

            string iconUrl(string icon)
            {
                return "https://openweathermap.org/img/wn/" + icon + "@2x.png";
            }
            Icon = iconUrl(weather.Icon);

            List<WeatherForecast> forecastListOneDay = new List<WeatherForecast>();
            List<WeatherForecast> forecastListTwoDays = new List<WeatherForecast>();
            List<WeatherForecast> forecastListThreeDays = new List<WeatherForecast>();
            List<WeatherForecast> forecastListFourDays = new List<WeatherForecast>();
            List<WeatherForecast> forecastListFiveDays = new List<WeatherForecast>();

            var todayOneDay = Today.AddDays(1);
            var todayTwoDays = Today.AddDays(2);
            var todayThreeDays = Today.AddDays(3);
            var todayFourDays = Today.AddDays(4);
            var todayFiveDays = Today.AddDays(5);

            foreach (WeatherForecast forecast in weatherForecast)
            {
                var dt = DateTime.ParseExact(forecast.Date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                if ((dt - todayOneDay).Days == 0)
                {
                    forecastListOneDay.Add(forecast);
                }
                else if ((dt - todayOneDay).Days == 1)
                {
                    forecastListTwoDays.Add(forecast);
                }
                else if ((dt - todayOneDay).Days == 2)
                {
                    forecastListThreeDays.Add(forecast);
                }
                else if ((dt - todayOneDay).Days == 3)
                {
                    forecastListFourDays.Add(forecast);
                }
                else if ((dt - todayOneDay).Days == 4)
                {
                    forecastListFiveDays.Add(forecast);
                }
                else break;
            }

            //Get Min Temperature From One Day
            int minTempOneDay = forecastListOneDay.Min(f => f.MinTemp);
            int minTempTwoDays = forecastListTwoDays.Min(f => f.MinTemp);
            int minTempThreeDays = forecastListThreeDays.Min(f => f.MinTemp);
            int minTempFourDays = forecastListFourDays.Min(f => f.MinTemp);
            int minTempFiveDays = forecastListFiveDays.Min(f => f.MinTemp);

            //Get Max Temperature From One Day
            int maxTempOneDay = forecastListOneDay.Max(f => f.MaxTemp);
            int maxTempTwoDays = forecastListTwoDays.Max(f => f.MaxTemp);
            int maxTempThreeDays = forecastListThreeDays.Max(f => f.MaxTemp);
            int maxTempFourDays = forecastListFourDays.Max(f => f.MaxTemp);
            int maxTempFiveDays = forecastListFiveDays.Max(f => f.MaxTemp);

            //Get Most Ocurring Icon For Most Accurate Weather Icon
            string getMostOcurringIcon(List<WeatherForecast> forecastsList)
            {
                var query = forecastsList.GroupBy(f => f.Icon)
                    .Select(group => new { icon = group.Key, Count = group.Count() })
                    .OrderByDescending(x => x.Count);
                var item = query.First();
                return item.icon;
            }

            string iconOneDay = getMostOcurringIcon(forecastListOneDay);
            string iconTwoDays = getMostOcurringIcon(forecastListTwoDays);
            string iconThreeDays = getMostOcurringIcon(forecastListThreeDays);
            string iconFourDays = getMostOcurringIcon(forecastListFourDays);
            string iconFiveDays = getMostOcurringIcon(forecastListFiveDays);

            List<WeatherForecast> finalForecasts = new List<WeatherForecast>();

            WeatherForecast forecastOneDay = new WeatherForecast();
            forecastOneDay.Date = "Tomorrow";
            forecastOneDay.MinTemp = minTempOneDay;
            forecastOneDay.MaxTemp = maxTempOneDay;
            forecastOneDay.Icon = iconUrl(iconOneDay);
            finalForecasts.Add(forecastOneDay);

            WeatherForecast forecastTwoDays = new WeatherForecast();
            forecastTwoDays.Date = todayTwoDays.DayOfWeek.ToString();
            forecastTwoDays.MinTemp = minTempTwoDays;
            forecastTwoDays.MaxTemp = maxTempTwoDays;
            forecastTwoDays.Icon = iconUrl(iconTwoDays);
            finalForecasts.Add(forecastTwoDays);

            WeatherForecast forecastThreeDays = new WeatherForecast();
            forecastThreeDays.Date = todayThreeDays.DayOfWeek.ToString();
            forecastThreeDays.MinTemp = minTempThreeDays;
            forecastThreeDays.MaxTemp = maxTempThreeDays;
            forecastThreeDays.Icon = iconUrl(iconThreeDays);
            finalForecasts.Add(forecastThreeDays);

            WeatherForecast forecastFourDays = new WeatherForecast();
            forecastFourDays.Date = todayFourDays.DayOfWeek.ToString();
            forecastFourDays.MinTemp = minTempFourDays;
            forecastFourDays.MaxTemp = maxTempFourDays;
            forecastFourDays.Icon = iconUrl(iconFourDays);
            finalForecasts.Add(forecastFourDays);

            WeatherForecast forecastFiveDays = new WeatherForecast();
            forecastFiveDays.Date = todayFiveDays.DayOfWeek.ToString();
            forecastFiveDays.MinTemp = minTempFiveDays;
            forecastFiveDays.MaxTemp = maxTempFiveDays;
            forecastFiveDays.Icon = iconUrl(iconFiveDays);
            finalForecasts.Add(forecastFiveDays);

            Forecasts = finalForecasts;
        }

        public void CheckFavorite()
        {
            if (App.Database.GetFavoriteAsync(City).Result != null)
            {
                Favorited = true;
                NotFavorited = !Favorited;
            }
            else
            {
                Favorited = false;
                NotFavorited = !Favorited;
            }
        }

        public void ToggleFavorite(object sender, System.EventArgs e)
        {
            var FavoriteCity = new FavoriteCity();
            FavoriteCity.City = City;
            FavoriteCity.Country = Country;
            if (App.Database.SaveItemAsync(FavoriteCity).Result >= 0)
            {
                Favorited = !Favorited;
                NotFavorited = !Favorited;
            }
        }
    }
}