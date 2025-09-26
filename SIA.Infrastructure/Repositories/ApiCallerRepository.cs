using SIA.Infrastructure.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SIA.Infrastructure.Repositories
{
    public class ApiCallerRepository(HttpClient httpClient) : IApiCallerRepository
    {
        public async Task<T?> GetAsync<T>(string url, string? bearerToken = null)
        {
            if (!string.IsNullOrEmpty(bearerToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task<T?> PostAsync<T>(string url, object payload, string? bearerToken = null)
        {
            if (!string.IsNullOrEmpty(bearerToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

    }
}
