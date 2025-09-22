namespace SIA.Domain.Entities
{
    public class AuthenticationResponse
    {
        public string UserRefId { get; set; }
        public string UserId { get; set; }
        public string UserDisplayName { get; set; }
        public string JwtToken { get; set; }
        public string UserSecurityKey { get; set; }
        public string UserRole { get; set; }
        public int ValidityTime { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsSignUpUser { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
        public string AESKey { get; set; }
    }
}
