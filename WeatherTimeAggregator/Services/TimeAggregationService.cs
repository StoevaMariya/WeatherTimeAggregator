using WeatherTimeAggregator.Interfaces;
using WeatherTimeAggregator.Models;

namespace WeatherTimeAggregator.Services
{
    public class TimeAggregationService
    {
        private readonly IEnumerable<ITimeProvider> _providers;

        public TimeAggregationService(IEnumerable<ITimeProvider> providers)
        {
            _providers = providers;
        }

        public async Task<AggregatedTimeResult> GetAggregatedTimeAsync(string city)
        {
            var tasks = _providers.Select(p => p.GetTimeAsync(city));
            var results = await Task.WhenAll(tasks);

            var avgTicks = (long)results.Average(r => r.Time.Ticks);

            var final = new DateTime(avgTicks, DateTimeKind.Utc);

            return new AggregatedTimeResult
            {
                ProviderResults = results.ToList(),
                FinalTime = final
            };
        }
    }
}
