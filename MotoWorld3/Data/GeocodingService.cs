using Newtonsoft.Json.Linq;

namespace MotoWorld3.Data
{
    public class GeocodingService
    {
        private readonly HttpClient _httpClient;

        public GeocodingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(double lat, double lon)> GetCoordinatesAsync(string street, string city, int postalcode, string country)
        {
            using var client = _httpClient;
            string url = $"https://nominatim.openstreetmap.org/search?street={Uri.EscapeDataString(street)}&city={Uri.EscapeDataString(city)}&postalcode={postalcode}&country={Uri.EscapeDataString(country)}&format=json";

            client.DefaultRequestHeaders.Add("User-Agent", "MyAspNetCoreApp/1.0");

            var response = await client.GetStringAsync(url);
            var json = JArray.Parse(response);

            if (json.Count > 0)
            {
                double lat = (double)json[0]["lat"];
                double lon = (double)json[0]["lon"];
                return (lat, lon);
            }

            return (0, 0);
        }
    }
}
