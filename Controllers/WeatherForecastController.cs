using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace weather_forecast_api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    string key = "8ef4036aff7ca93866c5cea003854060";

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<WeatherForecastModel> Get(string cityName)
    {
        var weather = await this.GetFromWeatherForecastApi(cityName);
        return weather;
    }


    private async Task<WeatherForecastModel> GetFromWeatherForecastApi(string cityName)
    {
        var weather = new WeatherForecastModel();
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        using (var httpClient = new HttpClient())
        {
            weather = JsonSerializer.Deserialize<WeatherForecastModel>(
                await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={key}&units=metric&lang=tr&trk=public_post_comment-text"), serializeOptions);
        }
        return weather;

    }
}