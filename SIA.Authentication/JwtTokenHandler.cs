using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SIA.Authentication
{
    public class JwtTokenHandler(JwtTokenParameter jwtTokenParameter) : IJwtTokenHandler
    {
        public async Task<AuthenticationResponse> GenerateToken(AuthenticationResponse authenticationResponse, bool isTemporary = false)
        {
            if (string.IsNullOrEmpty(authenticationResponse.UserRowId) ||
                string.IsNullOrEmpty(authenticationResponse.UserId) ||
                string.IsNullOrEmpty(authenticationResponse.UserRole) ||
                string.IsNullOrEmpty(authenticationResponse.SecurityKey))
                return new AuthenticationResponse() { IsSuccess = false, Message = "User authentication failed." };

            byte[] tokenKey = Encoding.ASCII.GetBytes(jwtTokenParameter.JwtSecurityKey);

            ClaimsIdentity claimsIdentity = new(
            [
                new(ClaimTypes.Name, authenticationResponse.UserRowId.ToString()),
                new(ClaimTypes.Role, authenticationResponse.UserRole),
                new("UserId", authenticationResponse.UserId),
                new("SecurityKey", authenticationResponse.SecurityKey),
                new("Origin", jwtTokenParameter.Origin)
            ]);

            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Subject = claimsIdentity,
                Issuer = jwtTokenParameter.ValidIssuer,
                Audience = jwtTokenParameter.ValidAudience,
                Expires = DateTime.UtcNow.AddMinutes(isTemporary == false ? jwtTokenParameter.TokenValidityInMinutes : 5),
                SigningCredentials = signingCredentials,
            };

            JwtSecurityTokenHandler securityTokenHandler = new();
            SecurityToken securityToken = securityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = securityTokenHandler.WriteToken(securityToken);

            authenticationResponse.JwtToken = token;
            authenticationResponse.IsSuccess = true;

            return await Task.FromResult(authenticationResponse);
        }
    }
}
