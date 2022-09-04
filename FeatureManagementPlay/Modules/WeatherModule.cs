using System;
using Carter;
using Microsoft.FeatureManagement;

namespace FeatureManagementPlay.Modules
{
    public class WeatherModule : ICarterModule
    {
        public WeatherModule()
        {
        }

        /*
         * 
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", (HttpContext httpContext) =>
        {
            var forecast =  Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                })
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast");
         * */

        string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/weatherforecast/random", RandomForecast);
        }

        async Task<IResult> RandomForecast(HttpContext httpContext, IFeatureManager featureManager)
        {
            if (!await featureManager.IsEnabledAsync("WeathreForecastRandom"))
            {
                return Results.NotFound();
            }

            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = _summaries[Random.Shared.Next(_summaries.Length)]
                })
                .ToArray();

            return Results.Ok(forecast);
        }
    }
}

