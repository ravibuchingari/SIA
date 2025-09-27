using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIA.Domain.Entities
{
    public class UserVM
    {
        public long UserId { get; set; }
        public Guid UserGuid { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string ProfileImageUrl { get; set; } = null!;
        public string HashPassword { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public string TimeZone { get; set; } = null!;
        public string Language { get; set; } = null!;
        public string TimeFormat { get; set; } = null!;
        public string DateFormat { get; set; } = null!;
        public byte RoleId { get; set; }
        public bool IsSignUpUser { get; set; }
        public bool IsEmailVerified { get; set; }
        public string? SecretKey { get; set; }
        public string? SecurityKey { get; set; }
        public byte UserStatusId { get; set; }
        public string? SocialAuthId { get; set; }
        public byte? SubscriptionId { get; set; }
        public int? OrganizationId { get; set; }
        public string? Message { get; set; }
        public OrganizationVM? OrganizationVM { get; set; }
    }
}
