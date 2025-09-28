namespace SIA.Domain.Models
{
    public class SignInRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecurityKey { get; set; }
        public string SecretKey { get; set; }
    }
}
