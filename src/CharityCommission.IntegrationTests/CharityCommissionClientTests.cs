using FluentAssertions;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace CharityCommission.IntegrationTests;

public class CharityCommissionClientTests
{
    public CharityCommissionClientTests(ITestOutputHelper testOutputHelper)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.TestOutput(testOutputHelper)
            .CreateLogger();
    }

    [Fact(Skip = "Requires API Key")]
    public async Task Can_get_Charity()
    {
        var settings = new CharityCommissionSettings
        {
            SubscriptionKey = ""
        };
        
        const string number = "235351";
        
        var client = new CharityCommissionClient(settings);
        var charity = await client.GetCharityAsync(number);

        charity.Should().NotBeNull();
        charity.Number.Should().Be(number);
    }
}