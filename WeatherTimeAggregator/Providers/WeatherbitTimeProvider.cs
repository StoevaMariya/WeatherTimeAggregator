using System.Net.Http.Json;
using WeatherTimeAggregator.Interfaces;
using WeatherTimeAggregator.Models;

namespace WeatherTimeAggregator.Providers
{
    public class WeatherbitTimeProvider : ITimeProvider
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public WeatherbitTimeProvider(HttpClient client, IConfiguration config)
        {
            _client = client;
            _apiKey = config["APIKeys:Weatherbit"]
                      ?? throw new ArgumentNullException("APIKeys:Weatherbit");
        }

        public async Task<TimeResult> GetTimeAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty");

            var encodedCity = Uri.EscapeDataString(city);

            
            var url = $"https://api.weatherbit.io/v2.0/current?city={encodedCity}&key={_apiKey}";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<WeatherbitResponse>();

            if (data == null || data.data == null || data.data.Length == 0)
                throw new InvalidOperationException("Invalid response from Weatherbit.io");

            var timestamp = data.data[0].ts; 

            var time = DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;

            return new TimeResult
            {
                Source = "Weatherbit",
                Time = time
            };
        }

        private class WeatherbitResponse
        {
            public WeatherbitData[] data { get; set; }
        }

        private class WeatherbitData
        {
            public long ts { get; set; }
        }
    }
}
