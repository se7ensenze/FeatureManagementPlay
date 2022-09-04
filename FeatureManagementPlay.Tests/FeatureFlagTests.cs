using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace FeatureManagementPlay.Tests;


public class UnitTest1
{
    [Fact(DisplayName = "Should produce HttpStatus OK When WeathreForecastRandom is on")]
    public async Task Test1()
    {

        using var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder((builder) =>
            {
                builder.ConfigureAppConfiguration((context, cfg) =>
                {
                    cfg.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["FeatureFlags:WeathreForecastRandom"] = "true"
                    });
                });
            });

        using var client = factory.CreateClient();

        var result = await client.GetAsync("/weatherforecast/random");

        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }

    [Fact(DisplayName = "Should produce HttpStatus NotFound When WeathreForecastRandom is off")]
    public async Task Test2()
    {

        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder((builder) =>
            {
                builder.ConfigureAppConfiguration((context, cfg) =>
                {
                    cfg.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["FeatureFlags:WeathreForecastRandom"] = "false"
                    });
                });
            });

        var client = factory.CreateClient();

        var result = await client.GetAsync("/weatherforecast/random");

        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
    }
}
