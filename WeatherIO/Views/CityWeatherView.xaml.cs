using WeatherIO.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherIO.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityWeatherView : ContentPage
    {
        public CityWeatherView(string city, string country)
        {
            InitializeComponent();
            var vm = new CityWeatherViewModel(city, country);
            BindingContext = vm;
            vm.UpdateWeather();
        }
    }
}