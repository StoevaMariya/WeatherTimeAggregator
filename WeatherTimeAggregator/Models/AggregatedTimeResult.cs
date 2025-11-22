namespace WeatherTimeAggregator.Models
{
    public class AggregatedTimeResult
    {
        public List<TimeResult> ProviderResults { get; set; } = new();
        public DateTime FinalTime { get; set; }
    }
}
