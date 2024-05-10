using Microsoft.AspNetCore.Mvc;
using AdamTibi.OpenWeather;
using System;
using Uqs.Weather.Wrappers;
namespace Uqs.Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private const int FORECAST_DAYS = 5;
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration _config;
    private readonly IClient _client;
    private readonly INowWrapper _nowWrapper;
    private readonly IRandomWrapper _random;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, IClient client, INowWrapper nowWrapper, IRandomWrapper random)
    {
        _logger = logger;
        _config = config;
        _client = client;
        _nowWrapper = nowWrapper;
        _random = random;
    }

    private string MapFeelToTemp(int temperatureC)
    {
        if (temperatureC <= 0)
            return Summaries.First();
        int summariesIndex = (temperatureC/5) + 1;
        if(summariesIndex >= Summaries.Length)
            return Summaries.Last();
        return Summaries[summariesIndex];
    }

    [HttpGet("GetRandomWeatherForecast")]
    public IEnumerable<WeatherForecast> GetRandom()
    {
        WeatherForecast[] wfs = new WeatherForecast[FORECAST_DAYS];
        for(int i = 0;i < wfs.Length;i++)
        {
            var wf = wfs[i] = new WeatherForecast();
            wf.Date = _nowWrapper.Now.AddDays(i + 1);
            wf.TemperatureC = _random.Next(-20, 55);
            wf.Summary = MapFeelToTemp(wf.TemperatureC);
        }
        return wfs;
    }

    [HttpGet("GetRealWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> GetReal()
    {
        const decimal GREENWICH_LAT = 51.4810m;
        const decimal GREENWICH_LON = 0.0052m;
        //string apiKey = _config["OpenWeather:Key"];
        //HttpClient httpClient = new HttpClient();
        //TestClient openWeatherClient = new TestClient(apiKey, httpClient);
        /*OneCallResponse res = await openWeatherClient.OneCallAsync
            (GREENWICH_LAT, GREENWICH_LON, new[] {
                Excludes.Current, Excludes.Minutely,
                Excludes.Hourly, Excludes.Alerts }, Units.Metric);*/

        OneCallResponse res = await _client.OneCallAsync
            (GREENWICH_LAT, GREENWICH_LON, new[] {
                Excludes.Current, Excludes.Minutely,
                Excludes.Hourly, Excludes.Alerts }, Units.Metric);

        WeatherForecast[] wfs = new WeatherForecast[FORECAST_DAYS];
        for (int i = 0; i < wfs.Length; i++)
        {
            var wf = wfs[i] = new WeatherForecast();
            wf.Date = res.Daily[i + 1].Dt;
            double forecastedTemp = res.Daily[i + 1].Temp.Day;
            wf.TemperatureC = (int)Math.Round(forecastedTemp);
            wf.Summary = MapFeelToTemp(wf.TemperatureC);
        }
        return wfs;
    }
    [HttpGet("ConvertCToF")]
    public double ConvertCToF(double c)
    {
        double f = c * (9d / 5d) + 32;
        _logger.LogInformation("conversion requested");
        return f;
    }

    /*[HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }*/

    /*[HttpGet(Name = "GetAlmostRandomWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> GetAlmostRandom(int m)
    {
        WeatherForecast[] wfs = new WeatherForecast[FORECAST_DAYS];
        for (int i = 0; i < wfs.Length; i++)
        {
            var wf = wfs[i] = new WeatherForecast();
            wf.Date = DateTime.Now.AddDays(i * 2 + m);
            wf.TemperatureC = Random.Shared.Next(-1, 15);
            wf.Summary = MapFeelToTemp(wf.TemperatureC);
        }
        return wfs;
    }*/
}
