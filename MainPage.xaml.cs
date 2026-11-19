using WeatherApp.Services;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private readonly LocationService _locationService;
        private readonly WeatherService _weatherService;

        public MainPage()
        {
            InitializeComponent();
            _locationService = new LocationService();
            _weatherService = new WeatherService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadWeatherAsync();
        }

        private async void OnGetWeatherClicked(object sender, EventArgs e)
        {
            await LoadWeatherAsync();
        }

        private async Task LoadWeatherAsync()
        {
            try
            {
                GetWeatherButton.IsEnabled = false;
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                var location = await _locationService.GetCurrentLocationAsync();
                var weather = await _weatherService.GetCurrentWeatherAsync(location.Latitude, location.Longitude);

                CityLabel.Text = weather.CityName;
                TempLabel.Text = $"{weather.Main.Temperature:F0}°";
                DescriptionLabel.Text = weather.Weather.Count > 0 ? weather.Weather[0].Description : "";
                HumidityLabel.Text = $"Humidity: {weather.Main.Humidity}%";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Could not get weather: {ex.Message}", "OK");
            }
            finally
            {
                GetWeatherButton.IsEnabled = true;
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;
            }
        }
    }
}