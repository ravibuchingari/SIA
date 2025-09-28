using SIA.Domain.Models;
using System.Security.Claims;

namespace SIA.Authentication
{
    public interface IJwtTokenHandler
    {
        Task<TokenResponse> GenerateTokenAsync(string userId, string userGuId, string role, string securityKey);
        Task<TokenResponse> GenerateTokenByClaimsAcync(IEnumerable<Claim> claims);
        string GetJwtSecurityKey();
    }
}