using App.CancellationTokenApi.Infrastructer.Providers.CancellationToken;
using Microsoft.AspNetCore.Mvc;
namespace App.CancellationTokenApi.Controllers;


[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ICancellationTokenProvider _cancellationTokenProvider;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ICancellationTokenProvider cancellationTokenProvider)
    {
        _logger = logger;
        _cancellationTokenProvider = cancellationTokenProvider;
    }


    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var cancellationToken = _cancellationTokenProvider.GetCancellationToken();

            for (int i = 1; i < 10; i++)
            {
                await Task.Delay(1000, cancellationToken);
            }

            IEnumerable<WeatherForecast> weatherForecast_list = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(weatherForecast_list);
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine("OperationCanceledException : " + e);
            return StatusCode(500, new
            {
                Message = e.Message
            });
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception : " + e);
            return StatusCode(500, new
            {
                Message = e.Message
            });
        }
    }
}