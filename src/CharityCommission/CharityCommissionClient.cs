﻿using CharityCommission.Model;
using Serilog;

namespace CharityCommission;

public class CharityCommissionClient : IDisposable
{
    private readonly ThreadLocal<HttpClient> _httpClient;

    public CharityCommissionClient(CharityCommissionSettings settings) : this(HttpClientFactory.Create(settings))
    {
    }

    private CharityCommissionClient(ThreadLocal<HttpClient> httpClient) => _httpClient = httpClient;

    public async Task<Charity> GetCharityAsync(string number, CancellationToken cancellationToken = default)
    {
        Log.Debug("Getting Charity - Number: {Number}", number);
        
        var charity  = await _httpClient.Value.GetResourceAsync<Charity>($"/register/api/charitydetails/{number}/0", cancellationToken).ConfigureAwait(false);
        
        Log.Debug("Got Charity - Number: {Number}, Charity: {@Charity}", number, charity);
        
        return charity;
    }

    public void Dispose() => _httpClient.Dispose();
}