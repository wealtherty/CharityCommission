using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CharityCommission.IntegrationTests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_ => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, false)
            .AddUserSecrets(typeof(Startup).Assembly)
            .Build());
        
        services.AddSingleton(provider =>
        {
            var configuration = provider.GetRequiredService<IConfigurationRoot>();
            var section = configuration.GetSection(typeof(CharityCommissionClient).Namespace);
            return section.Get<CharityCommissionSettings>();
        });
        services.AddSingleton(provider =>
        {
            var settings = provider.GetRequiredService<CharityCommissionSettings>();

            return new CharityCommissionClient(settings);
        });
        services.AddSingleton<Fixture>();
    }
}