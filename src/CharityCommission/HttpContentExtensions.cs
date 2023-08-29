using Newtonsoft.Json;

namespace CharityCommission;

public static class HttpContentExtensions
{
    public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
    {
        await using var stream = await content.ReadAsStreamAsync()
            .ConfigureAwait(false);
        using var streamReader = new StreamReader(stream);
        await using var jsonTextReader = new JsonTextReader(streamReader);
        var serializer = new JsonSerializer();

        return serializer.Deserialize<T>(jsonTextReader);
    }
}