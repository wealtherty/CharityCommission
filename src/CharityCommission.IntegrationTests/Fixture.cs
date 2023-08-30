using Serilog;
using Xunit.Abstractions;

namespace CharityCommission.IntegrationTests;

public class Fixture
{
    public CharityCommissionClient Client { get; }

    public Fixture(ITestOutputHelper testOutputHelper, CharityCommissionClient client)
    {
        Client = client;

        InitialiseLogging(testOutputHelper);
    }

    private static void InitialiseLogging(ITestOutputHelper testOutputHelper)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.TestOutput(testOutputHelper)
            .CreateLogger();
    }
}