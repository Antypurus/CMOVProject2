using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherIO.Views;

namespace WeatherIO
{
    public partial class App : Application
    {
        static FavoriteCityDatabase database;
        public App()
        {
            InitializeComponent();

            Resources = new ResourceDictionary();
            Resources.Add("purple", Color.FromHex("7454C7"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var nav = new NavigationPage(new FavoritesListView());
            nav.BarBackgroundColor = (Color)App.Current.Resources["purple"];
            nav.BarTextColor = Color.White;

            MainPage = nav;
        }

        public static FavoriteCityDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FavoriteCityDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("WeatherIOSQLite.db3"));
                }
                return database;
            }
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
