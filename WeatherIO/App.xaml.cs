using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherIO.Views;

namespace WeatherIO
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Resources = new ResourceDictionary();
            Resources.Add("grey", Color.FromHex("D8DDF1"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var nav = new NavigationPage(new CityWeatherView("Porto", "PT"));
            nav.BarBackgroundColor = (Color)App.Current.Resources["grey"];
            nav.BarTextColor = Color.White;

            MainPage = nav;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
