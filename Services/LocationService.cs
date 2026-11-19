namespace WeatherApp.Services
{
    public class LocationService
    {
        public async Task<Location> GetCurrentLocationAsync()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
                throw new UnauthorizedAccessException("Location permission was not granted.");

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.Default.GetLocationAsync(request);

            if (location == null)
                throw new Exception("Unable to retrieve location.");

            return location;
        }
    }
}