using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CharityCommission.IntegrationTests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = GetConfigurationRoot();
        var section = configuration.GetSection(typeof(CharityCommissionClient).Namespace);
        var settings = section.Get<CharityCommissionSettings>();
        
        services.AddSingleton(settings);
        services.AddSingleton<ITestOutputHelper, TestOutputHelper>();
        services.AddSingleton(provider =>
        {
            var settings = provider.GetRequiredService<CharityCommissionSettings>();

            return new CharityCommissionClient(settings);
        });
        services.AddSingleton<Fixture>();
    }
    
    private static IConfigurationRoot GetConfigurationRoot()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, false)
            .AddUserSecrets(typeof(Startup).Assembly)
            .Build();
    }

}