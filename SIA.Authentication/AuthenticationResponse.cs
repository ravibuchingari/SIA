namespace SIA.Authentication
{
    public class AuthenticationResponse
    {
        public string UserRowId { get; set; }
        public string ClientName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public string SecurityKey { get; set; }
        public string UserRole { get; set; }
        public int ValidityTime { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
