namespace Domain.Models
{
    public class WeatherForecast 
    {
        public string Date { get; set; }

        public float MinTemp { get; set; }

        public float MaxTemp { get; set; }

        public float WindSpeed { get; set; }

        public float WindDegree { get; set; }

        public float Humidity { get; set; }

        public string Icon { get; set; }
    }
}