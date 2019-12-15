using WeatherIO.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherIO.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityWeather : ContentPage
    {
        public CityWeather()
        {
            InitializeComponent();
            BindingContext = new CityWeatherViewModel();
        }
    }
}