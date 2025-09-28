namespace SIA.Domain.Models
{
    public class SignInRequest
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? SecurityKey { get; set; }
        public string? SecretKey { get; set; }
    }
}
