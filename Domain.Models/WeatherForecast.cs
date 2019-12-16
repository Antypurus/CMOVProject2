namespace Domain.Models
{
    public class WeatherForecast 
    {
        public string Date { get; set; }

        public int MinTemp { get; set; }

        public int MaxTemp { get; set; }

        public float WindSpeed { get; set; }

        public float WindDegree { get; set; }

        public float Humidity { get; set; }

        public string Icon { get; set; }
    }
}