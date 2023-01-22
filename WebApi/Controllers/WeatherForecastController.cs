using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger _logger;

    public WeatherForecastController(ILogger logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        try
        {
            var random = new Random().Next(0, 5);
            if (random < 2)
                throw new Exception("Random exception occured");
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Something went wrong! {CustomProperty}", 50);
            return new StatusCodeResult(500);
        }
    }
}
