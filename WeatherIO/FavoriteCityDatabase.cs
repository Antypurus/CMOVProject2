using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace WeatherIO {
	public class FavoriteCityDatabase {
		readonly SQLiteAsyncConnection database;

		public FavoriteCityDatabase(string dbpath)
		{
			database = new SQLiteAsyncConnection(dbpath);
			database.CreateTableAsync<FavoriteCity>().Wait();
		}

		public Task<List<FavoriteCity>> GetFavoritesAsync() {
			return database.Table<FavoriteCity>().ToListAsync();
		}

		public Task<List<FavoriteCity>> GetItemsNotDoneAsync() {
			return database.QueryAsync<FavoriteCity>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
		}

		public Task<FavoriteCity> GetFavoriteAsync(string city) {
			return database.Table<FavoriteCity>().Where(i => i.City == city).FirstOrDefaultAsync();
		}

		public Task<int> SaveItemAsync(FavoriteCity city) {
			if (city.City != null)
				return database.UpdateAsync(city);
			else
				return database.InsertAsync(city);
		}

		public Task<int> DeleteItemAsync(FavoriteCity city) {
			return database.DeleteAsync(city);
		}
	}
}

