using Serilog;
using Serilog.Core;
using Xunit.DependencyInjection;

namespace CharityCommission.IntegrationTests;

public class Fixture
{
    private readonly ITestOutputHelperAccessor _testOutputHelperAccessor;
    private readonly CharityCommissionClient _client;

    public const string ReasonToSkip = "Requires API key";
    
    public Fixture(CharityCommissionClient client, ITestOutputHelperAccessor testOutputHelperAccessor)
    {
        _client = client;
        _testOutputHelperAccessor = testOutputHelperAccessor;
    }
    
    public CharityCommissionClient GetClient()
    {
        Initialise();
        return _client;
    }

    private void Initialise()
    {
        if (Log.Logger != Logger.None) return;
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.TestOutput(_testOutputHelperAccessor.Output)
            .CreateLogger();
    }
    
}