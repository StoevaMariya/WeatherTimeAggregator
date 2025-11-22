using Microsoft.AspNetCore.Mvc;
using WeatherTimeAggregator.Services;

namespace WeatherTimeAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly TimeAggregationService _service;

        public TimeController(TimeAggregationService service)
        {
            _service = service;
        }

        // GET /api/time/sofia
        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            var result = await _service.GetAggregatedTimeAsync(city);
            return Ok(result);
        }
    }
}
