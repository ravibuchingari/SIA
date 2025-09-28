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
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly IConfiguration configuration;
        private readonly JwtTokenParameter jwtTokenParameter;

        public JwtTokenHandler(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            jwtTokenParameter = new JwtTokenParameter()
            {
                JwtSecurityKey = configuration["JWTSettings:JWTKey"]!,
                IsValidateIssuer = Convert.ToBoolean(configuration["JWTSettings:IsValidIssuer"]),
                IsValidateAudience = Convert.ToBoolean(configuration["JWTSettings:IsValidAudience"]),
                ValidIssuer = configuration["JWTSettings:ValidIssuer"]!,
                ValidAudience = configuration["JWTSettings:ValidAudience"]!,
                TokenValidityInMinutes = Convert.ToDouble(configuration["JWTSettings:JWTTokenValidityInMinutes"]),
                RefreshTokenValidityInMinutes = Convert.ToInt32(configuration["JWTSettings:RefreshTokenValidityInMinutes"])
            };
        }

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
                RefreshTokenValidityInMinutes = Convert.ToInt32(configuration["JWTSettings:RefreshTokenValidityInMinutes"])
            };


            ClaimsIdentity claimsIdentity = new(
            [
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, userId),
                new(ClaimTypes.Role, role),
                new("GuId", userGuId),
                new("SecurityKey", securityKey),
                new("Origin", jwtTokenParameter.Origin)
            ]);

            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenParameter.JwtSecurityKey)), SecurityAlgorithms.HmacSha256Signature);

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
                Expires = DateTime.Now.AddMinutes(jwtTokenParameter.RefreshTokenValidityInMinutes),
                Created = DateTime.Now,
                UserId = int.Parse(userId)
            };

            TokenResponse tokenResponse = new()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                IsSuccess = true
            };

            return await Task.FromResult(tokenResponse);
        }

        public async Task<TokenResponse> GenerateTokenByClaimsAcync(IEnumerable<Claim> claims)
        {
            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenParameter.JwtSecurityKey)), SecurityAlgorithms.HmacSha256Signature);

            var claimsDict = claims
                            .GroupBy(c => c.Type)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Count() == 1 ? (object)g.First().Value : g.Select(c => c.Value).ToArray());


            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Claims = claimsDict,
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
                Expires = DateTime.Now.AddMinutes(jwtTokenParameter.RefreshTokenValidityInMinutes),
                Created = DateTime.Now,
                UserId = Convert.ToInt32(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value)
            };

            TokenResponse tokenResponse = new()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                IsSuccess = true
            };

            return await Task.FromResult(tokenResponse);
        }

        public string GetJwtSecurityKey() => jwtTokenParameter.JwtSecurityKey;


    }
}
