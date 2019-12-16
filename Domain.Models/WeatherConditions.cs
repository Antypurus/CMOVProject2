namespace Domain.Models
{
    public class WeatherConditions
    {
        public string Description { get; set; }
        
        public float Temperature { get; set; }
        public float MinTemp { get; set; }

        public float MaxTemp { get; set; }

        public float Humidity { get; set; }

        public float WindSpeed { get; set; }

        public float WindDegree { get; set; }

        public float Pressure { get; set; }
    }
}