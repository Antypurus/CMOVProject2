using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherIO.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherIO.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortugalDistrictsView : ContentPage
    {
        public PortugalDistrictsView()
        {
            InitializeComponent();
            BindingContext = new PortugalDistrictsViewModel();
        }

        private async void CitySelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = ((Tuple<string, string>)e.SelectedItem);
                await Navigation.PushAsync(new CityWeatherView(item.Item1, item.Item2));
            }
        }
    }
}