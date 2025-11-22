using WeatherTimeAggregator.Models;

namespace WeatherTimeAggregator.Interfaces
{
    public interface ITimeProvider
    {
        Task<TimeResult> GetTimeAsync(string city);
    }
}
