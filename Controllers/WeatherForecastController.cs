using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;

namespace webapi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get([FromServices] MySqlConnection conn)
    {
        try
        {
            using (conn)
            {
                await conn.OpenAsync()

await conn.ExecuteAsync("sleep 2s")
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
