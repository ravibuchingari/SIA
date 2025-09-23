using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Domain.Entities
{
    public class UserVM
    {
        public Guid? UserId { get; set; } = null;
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Mail { get; set; } = null!;
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
    }
}
