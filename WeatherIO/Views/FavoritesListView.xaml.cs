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
    public partial class FavoritesListView : ContentPage
    {
        public FavoritesListView()
        {
            InitializeComponent();
            var vm = new FavoritesListViewModel();
            BindingContext = vm;
        }

        protected override void OnDisappearing()
        {
            listView.SelectedItem = null;
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            var vm = new FavoritesListViewModel();
            BindingContext = vm;
            base.OnAppearing();
        }

        async void ShowAllCities(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PortugalDistrictsView());
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = ((FavoriteCity)e.SelectedItem).Copy();
                var list = ((FavoritesListViewModel)BindingContext).Favorites;
                await Navigation.PushAsync(new CityWeatherView(item.City, item.Country));
            }
        }
    }
}