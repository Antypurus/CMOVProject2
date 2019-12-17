using System;
using SQLite;

namespace WeatherIO {
	public class FavoriteCity : IEquatable<FavoriteCity> {
		[PrimaryKey, AutoIncrement]

        public int ID { get; set; }
        public string City { get; set; }
		public string Country { get; set; }

    public FavoriteCity Copy() {
      var newFavoriteCity = new FavoriteCity();
      newFavoriteCity.ID = ID;
      if (City != null)
        newFavoriteCity.City = string.Copy(City);
      if (Country != null)
        newFavoriteCity.Country = string.Copy(Country);
      return newFavoriteCity;
    }

    public bool Equals(FavoriteCity other) {
      if (other == null)
        return false;
      if (ID == other.ID)
        return true;
      else
        return false;
    }
  }
}