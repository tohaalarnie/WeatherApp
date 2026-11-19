using System.Text.Json.Serialization;

namespace WeatherApp.Services
{
    public class WeatherResponse
    {
        [JsonPropertyName("name")]
        public string CityName { get; set; }

        [JsonPropertyName("main")]
        public MainInfo Main { get; set; }

        [JsonPropertyName("weather")]
        public List<WeatherInfo> Weather { get; set; }
    }

    public class MainInfo
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class WeatherInfo
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }
}