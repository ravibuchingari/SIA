using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SIA.Domain.Entities;
using SIA.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SIA.Authentication
{
    public class JwtTokenHandler(IConfiguration configuration) : IJwtTokenHandler
    {
        public static string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public async Task<TokenResponse> GenerateTokenAsync(string userId, string userGuId, string role, string securityKey)
        {
            if (string.IsNullOrEmpty(userId) ||
                string.IsNullOrEmpty(userGuId) ||
                string.IsNullOrEmpty(role) ||
                string.IsNullOrEmpty(securityKey))
                return new TokenResponse() { IsSuccess = false, Message = "Authentication failed." };

            JwtTokenParameter jwtTokenParameter = new()
            {
                JwtSecurityKey = configuration["JWTSettings:JWTKey"]!,
                IsValidateIssuer = Convert.ToBoolean(configuration["JWTSettings:IsValidIssuer"]),
                IsValidateAudience = Convert.ToBoolean(configuration["JWTSettings:IsValidAudience"]),
                ValidIssuer = configuration["JWTSettings:ValidIssuer"]!,
                ValidAudience = configuration["JWTSettings:ValidAudience"]!,
                TokenValidityInMinutes = Convert.ToDouble(configuration["JWTSettings:JWTTokenValidityInMinutes"]),
            };

            byte[] tokenKey = Encoding.ASCII.GetBytes(jwtTokenParameter.JwtSecurityKey);

            ClaimsIdentity claimsIdentity = new(
            [
                new(ClaimTypes.Name, userId),
                new(ClaimTypes.Role, role),
                new("GuId", userGuId),
                new("SecurityKey", securityKey),
                new("Origin", jwtTokenParameter.Origin)
            ]);

            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Subject = claimsIdentity,
                Issuer = jwtTokenParameter.ValidIssuer,
                Audience = jwtTokenParameter.ValidAudience,
                Expires = DateTime.UtcNow.AddMinutes(jwtTokenParameter.TokenValidityInMinutes),
                SigningCredentials = signingCredentials,
            };

            JwtSecurityTokenHandler securityTokenHandler = new();
            SecurityToken securityToken = securityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = securityTokenHandler.WriteToken(securityToken);

            RefreshTokenVM refreshToken = new()
            {
                Token = GenerateRefreshToken(),
                Expires = DateTime.UtcNow.AddDays(1),
                Created = DateTime.UtcNow,
                UserId = userId
            };

            TokenResponse tokenResponse = new()
            {
                JwtToken = token,
                RefreshToken = refreshToken,
                IsSuccess = true
            };

            return await Task.FromResult(tokenResponse);
        }
    }
}
