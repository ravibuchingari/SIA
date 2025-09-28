using SIA.Domain.Entities;

namespace SIA.Domain.Models
{
    public class TokenResponse
    {
        public string JwtToken { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public RefreshTokenVM RefreshToken { get; set; }
    }
}
