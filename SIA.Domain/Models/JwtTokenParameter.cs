namespace SIA.Domain.Models
{
    public class JwtTokenParameter
    {
        public string JwtSecurityKey { get; set; } = null!;
        public double TokenValidityInMinutes { get; set; }
        public string ValidIssuer { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public bool IsValidateIssuer { get; set; } = true;
        public bool IsValidateAudience { get; set; } = true;
        public bool IsValidateLifetime { get; set; } = true;
        public string Origin { get; set; } = string.Empty;
        public int RefreshTokenValidityInMinutes { get; set; } = 20;
    }
}
