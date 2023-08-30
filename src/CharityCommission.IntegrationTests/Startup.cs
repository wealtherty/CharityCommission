using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CharityCommission.IntegrationTests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ITestOutputHelper, TestOutputHelper>();
        services.AddSingleton(_ => new CharityCommissionSettings
        {
            SubscriptionKey = ""
        });
        services.AddSingleton(provider =>
        {
            var settings = provider.GetRequiredService<CharityCommissionSettings>();

            return new CharityCommissionClient(settings);
        });
        services.AddSingleton<Fixture>();
    }
}