using Microsoft.AspNetCore.Mvc;
using My.Test1;
using My.Test2;

namespace My.Dependcys.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITest1Service _test1Service;
        private readonly ITest2Service _test2Service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , ITest1Service test1Service
            , ITest2Service test2Service)
        {
            _logger = logger;
            _test1Service = test1Service;
            _test2Service = test2Service;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _test1Service.Show();
            _test2Service.Show();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}