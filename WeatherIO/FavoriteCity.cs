using System;
using SQLite;

namespace WeatherIO {
	public class FavoriteCity : IEquatable<FavoriteCity> {
		[PrimaryKey, AutoIncrement]
		public string City { get; set; }
		public string Country { get; set; }

    public FavoriteCity Copy() {
      var newFavoriteCity = new FavoriteCity();
      if (City != null)
        newFavoriteCity.City = string.Copy(City);
      if (Country != null)
        newFavoriteCity.Country = string.Copy(Country);
      return newFavoriteCity;
    }

    public bool Equals(FavoriteCity other) {
      if (other == null)
        return false;
      if (City == other.City)
        return true;
      else
        return false;
    }
  }
}