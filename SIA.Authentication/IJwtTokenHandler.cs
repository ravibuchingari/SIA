using SIA.Domain.Models;

namespace SIA.Authentication
{
    public interface IJwtTokenHandler
    {
        Task<TokenResponse> GenerateTokenAsync(string userId, string userGuId, string role, string securityKey);
    }
}