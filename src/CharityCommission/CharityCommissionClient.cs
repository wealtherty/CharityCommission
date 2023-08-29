using CharityCommission.Model;
using Serilog;

namespace CharityCommission;

public class CharityCommissionClient
{
    private readonly HttpClient _httpClient;

    public CharityCommissionClient(CharityCommissionSettings settings) : this(new HttpClientFactory(settings).Create())
    {
    }

    private CharityCommissionClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Charity> GetCharityAsync(string number, CancellationToken cancellationToken = default)
    {
        Log.Debug("Getting Charity - Number: {Number}", number);
        
        var response = await _httpClient.GetAsync($"/register/api/charitydetails/{number}/0", cancellationToken).ConfigureAwait(false);
        
        response.EnsureSuccessStatusCode();

        var charity = await response.Content.ReadAsJsonAsync<Charity>().ConfigureAwait(false);
        
        Log.Debug("Got Charity - Number: {Number}, Charity: {@Charity}", number, charity);
        
        return charity;
    }
}