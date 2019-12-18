using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace WeatherIO.ViewModels
{
    public class FavoritesListViewModel : BaseViewModel
    {
        ObservableCollection<FavoriteCity> favoritesList;
        private bool _emptyFavoritesList;

        public ObservableCollection<FavoriteCity> Favorites
        {
            set { SetProperty(ref favoritesList, value); }
            get
            {
                if (favoritesList == null)
                    Initialize();
                return favoritesList;
            }
        }

        public bool EmptyFavoritesList
        {
            get => _emptyFavoritesList;
            set
            {
                _emptyFavoritesList = value;
                NotifyPropertyChanged();
            }
        }

        private async void Initialize()
        {
            List<FavoriteCity> list = await WeatherIO.App.Database.GetFavoritesAsync();
            Favorites = new ObservableCollection<FavoriteCity>(list);
            if (Favorites.Count <= 0)
                EmptyFavoritesList = true;
        }
    }
}