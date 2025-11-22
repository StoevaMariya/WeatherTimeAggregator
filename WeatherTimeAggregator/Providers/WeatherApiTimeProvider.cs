using System.Net.Http.Json;
using WeatherTimeAggregator.Interfaces;
using WeatherTimeAggregator.Models;

namespace WeatherTimeAggregator.Providers
{
    public class WeatherApiTimeProvider : ITimeProvider
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public WeatherApiTimeProvider(HttpClient client, IConfiguration config)
        {
            _client = client;
            _apiKey = config["APIKeys:WeatherAPI"]
                      ?? throw new ArgumentNullException("APIKeys:WeatherAPI");
        }

        public async Task<TimeResult> GetTimeAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty");

            var encodedCity = Uri.EscapeDataString(city);

            
            var url = $"https://api.weatherapi.com/v1/current.json?key={_apiKey}&q={encodedCity}";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<WeatherApiResponse>();

            if (data == null)
                throw new InvalidOperationException("Invalid response from WeatherAPI");

            var time = DateTime.Parse(data.location.localtime).ToUniversalTime();

            return new TimeResult
            {
                Source = "WeatherAPI",
                Time = time
            };
        }

        private class WeatherApiResponse
        {
            public WeatherLocation location { get; set; }
        }

        private class WeatherLocation
        {
            public string localtime { get; set; }
        }
    }
}
