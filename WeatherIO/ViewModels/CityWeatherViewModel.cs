using System.ComponentModel;
using Weather.Application.Services;
using WeatherIO.Backend;

namespace WeatherIO.ViewModels
{
    public class CityWeatherViewModel : BaseViewModel
    {
        private readonly WeatherService _weatherService;
        private readonly string _city;
        private readonly string _country;
        private string _description;
        private string _temperature;
        private string _humidity;
        private string _windSpeed;
        private string _windDegree;
        private string _pressure;

        public CityWeatherViewModel(string city, string country)
        {
            _weatherService = new WeatherService();
            _city = city;
            _country = country;
        }

        public string Country => _country;

        public string City => _city;

        public string CityCountry => City + ", " + Country;

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

        public void UpdateWeather()
        {
            var weather = _weatherService.GetCityWeather(_city, _country);

            Description = weather.Description.ToString();
            Temperature = weather.Temperature.ToString();
            Humidity = $"{weather.Humidity.ToString()} %";
            WindSpeed = $"{weather.WindSpeed.ToString()} m/s";
            WindDegree = $"{weather.WindDegree.ToString()} °";
            Pressure = $"{weather.Pressure.ToString()} hpa";
        }
    }
}