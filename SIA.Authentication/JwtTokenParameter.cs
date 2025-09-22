namespace Authentication.JWTAuthenticationManager
{
    public class JwtTokenParameter
    {
        public string JwtSecurityKey { get; set; } = null!;
        public double TokenValidityInMinutes { get; set; }
        public string ValidIssuer { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public bool IsValidateIssuer { get; set; } = false;
        public bool IsValidateAudience { get; set; } = false;
        public string Origin { get; set; } = string.Empty;
    }
}
