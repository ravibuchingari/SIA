namespace Authentication.JWTAuthenticationManager
{
    public class RefreshCredentials
    {
        public string JWTToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string SecurityKey { get; set; } = string.Empty;
    }
}
