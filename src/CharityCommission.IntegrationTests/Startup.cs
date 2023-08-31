using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CharityCommission.IntegrationTests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_ => new ConfigurationBuilder()
            .AddUserSecrets(typeof(Startup).Assembly)
            .Build());
        
        services.AddSingleton(provider =>
        {
            var configurationRoot = provider.GetRequiredService<IConfigurationRoot>();

            var section = configurationRoot.GetSection(typeof(CharityCommissionClient).Namespace);
            return section.Get<CharityCommissionSettings>();
        });
        services.AddSingleton<CharityCommissionClient>();
        services.AddSingleton<Fixture>();
    }
}