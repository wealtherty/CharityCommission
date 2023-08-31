using Newtonsoft.Json;

namespace CharityCommission;

public static class HttpContentExtensions
{
    public static async Task<T> GetResourceAsync<T>(this HttpClient self, string requestUri, CancellationToken cancellationToken = default)
    {
        var response = await self.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        
        response.EnsureSuccessStatusCode();

        var resource = await response.Content.ReadAsJsonAsync<T>().ConfigureAwait(false);
        
        return resource;
    
    }

    private static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
    {
        await using var stream = await content.ReadAsStreamAsync()
            .ConfigureAwait(false);
        using var streamReader = new StreamReader(stream);
        await using var jsonTextReader = new JsonTextReader(streamReader);
        var serializer = new JsonSerializer();

        return serializer.Deserialize<T>(jsonTextReader);
    }
}