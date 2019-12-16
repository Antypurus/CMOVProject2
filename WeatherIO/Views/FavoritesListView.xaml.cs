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

        async void OnItemAdded(object sender, EventArgs e)
        {
            var oldBinding = (FavoritesListViewModel)BindingContext;
            await Navigation.PushAsync(new CityWeatherView("Porto", "PT"));
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = ((FavoriteCity)e.SelectedItem).Copy();
                var list = ((FavoritesListViewModel)BindingContext).Favorites;
                await Navigation.PushAsync(new CityWeatherView("Porto", "PT"));
            }
        }
    }
}