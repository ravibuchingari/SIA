using SIA.Domain.Entities;

namespace SIA.Infrastructure.Interfaces
{
    public interface IGlobalConfigRepository
    {
        Task<AuthConfigVM?> GetAuthConfigAsync(string provider);
    }
}
