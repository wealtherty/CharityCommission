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
        if (string.IsNullOrEmpty(_settings.SubscriptionKey))
        {
            throw new Exception(
                $"{nameof(CharityCommissionSettings.SubscriptionKey)} isn't populated.  Check {nameof(CharityCommissionSettings)}");
        }
        
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_settings.Uri)
        };
        httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _settings.SubscriptionKey);
        
        return httpClient; 
    }
}