using System.Text.Json;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private string _apiKey;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        private async Task<string> GetApiKeyAsync()
        {
            if (!string.IsNullOrEmpty(_apiKey))
                return _apiKey;

            using var stream = await FileSystem.OpenAppPackageFileAsync("appsettings.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            _apiKey = config["OpenWeatherApiKey"];
            return _apiKey;
        }

        public async Task<WeatherResponse> GetCurrentWeatherAsync(double latitude, double longitude)
        {
            string apiKey = await GetApiKeyAsync();
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherResponse>(json);
        }
    }
}