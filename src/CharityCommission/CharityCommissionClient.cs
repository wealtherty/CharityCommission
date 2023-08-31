using CharityCommission.Model;
using Serilog;

namespace CharityCommission;

public class CharityCommissionClient
{
    private readonly Func<HttpClient> _httpClientFunc;

    public CharityCommissionClient(CharityCommissionSettings settings) : this(() => new HttpClientFactory(settings).Create())
    {
    }

    private CharityCommissionClient(Func<HttpClient> httpClientFunc)
    {
        _httpClientFunc = httpClientFunc;
    }

    public async Task<Charity> GetCharityAsync(string number, CancellationToken cancellationToken = default)
    {
        Log.Debug("Getting Charity - Number: {Number}", number);
        
        var response = await _httpClientFunc().GetAsync($"/register/api/charitydetails/{number}/0", cancellationToken).ConfigureAwait(false);
        
        response.EnsureSuccessStatusCode();

        var charity = await response.Content.ReadAsJsonAsync<Charity>().ConfigureAwait(false);
        
        Log.Debug("Got Charity - Number: {Number}, Charity: {@Charity}", number, charity);
        
        return charity;
    }
}