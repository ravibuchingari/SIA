using System.ComponentModel.DataAnnotations;

namespace SIA.Domain.Entities
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "User Id cannot be empty,")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Id cannot be empty,")]
        public string Password { get; set; } = string.Empty;
        public string? saltKey { get; set; }
        public string? RefreshToken { get; set; } = string.Empty;
    }
}
