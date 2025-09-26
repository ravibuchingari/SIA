namespace SIA.Infrastructure.Interfaces
{
    public interface IApiCallerRepository
    {
        Task<T?> GetAsync<T>(string url, string? bearerToken = null);
        Task<T?> PostAsync<T>(string url, object payload, string? bearerToken = null);
    }
}
