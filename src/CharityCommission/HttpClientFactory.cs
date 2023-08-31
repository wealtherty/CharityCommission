namespace CharityCommission;

public static class HttpClientFactory
{
    public static ThreadLocal<HttpClient> Create(CharityCommissionSettings settings)
    {
        return new ThreadLocal<HttpClient>(() =>
        {
            if (string.IsNullOrEmpty(settings.SubscriptionKey))
            {
                throw new Exception(
                    $"{nameof(CharityCommissionSettings.SubscriptionKey)} isn't populated.  Check {nameof(CharityCommissionSettings)}");
            }

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(settings.Uri)
            };
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", settings.SubscriptionKey);

            return httpClient;
        });
    }
}