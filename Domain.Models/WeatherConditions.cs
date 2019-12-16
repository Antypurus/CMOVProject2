namespace Domain.Models
{
    public class WeatherConditions
    {
        public string Description { get; set; }
        
        public int Temperature { get; set; }
        public int MinTemp { get; set; }

        public int MaxTemp { get; set; }

        public float Humidity { get; set; }

        public float WindSpeed { get; set; }

        public float WindDegree { get; set; }

        public float Pressure { get; set; }

        public string Icon { get; set; }
    }
}