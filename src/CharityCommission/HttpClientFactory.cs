namespace CharityCommission;

public class HttpClientFactory
{
    private readonly CharityCommissionSettings _settings;

    public HttpClientFactory(CharityCommissionSettings settings)
    {
        _settings = settings;
    }

    public HttpClient Create()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_settings.Uri)
        };
        httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _settings.SubscriptionKey);
        
        return httpClient; 
    }
}