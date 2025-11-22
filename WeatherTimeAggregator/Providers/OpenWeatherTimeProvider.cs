using System.Net.Http.Json;
using WeatherTimeAggregator.Interfaces;
using WeatherTimeAggregator.Models;

namespace WeatherTimeAggregator.Providers
{
    public class OpenWeatherTimeProvider : ITimeProvider
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public OpenWeatherTimeProvider(HttpClient client, IConfiguration configuration)
        {
            _client = client;

            _apiKey = configuration["APIKeys:OpenWeather"]
                      ?? throw new ArgumentNullException("APIKeys:OpenWeather");
        }

        public async Task<TimeResult> GetTimeAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty");

            var encodedCity = Uri.EscapeDataString(city);

            
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={encodedCity}&appid={_apiKey}";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<OpenWeatherResponse>();

            if (data == null)
                throw new InvalidOperationException("Invalid response from OpenWeatherMap");

            var time = DateTimeOffset.FromUnixTimeSeconds(data.dt).UtcDateTime;

            return new TimeResult
            {
                Source = "OpenWeatherMap",
                Time = time
            };
        }

        private class OpenWeatherResponse
        {
            public long dt { get; set; }    
        }
    }
}
