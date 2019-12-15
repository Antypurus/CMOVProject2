using System.ComponentModel;
using Weather.Application.Services;
using WeatherIO.Backend;

namespace WeatherIO.ViewModels
{
    public class CityWeatherViewModel : BaseViewModel
    {
        private readonly WeatherService _weatherService;
        private string _temperature;
        private string _humidity;
        private string _windSpeed;
        private string _windDegree;
        private string _pressure;

        public CityWeatherViewModel() => _weatherService = new WeatherService();

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

        public void UpdateWeather(string city, string country)
        {
            var weather = _weatherService.GetCityWeather(city, country);

            Temperature = $"{weather.Humidity.ToString()}";
            Humidity = $"{weather.Temperature.ToString()} %";
            WindSpeed = $"{weather.WindSpeed.ToString()} m/s";
            WindDegree = $"{weather.WindDegree.ToString()} °";
            Pressure = $"{weather.Pressure.ToString()} hpa";
        }
    }
}