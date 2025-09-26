using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIA.Infrastructure.DTO;

[Index("Email", Name = "UQ__Users__2724B2D19BCC564E", IsUnique = true)]
[Index("Username", Name = "UQ__Users__536C85E425BF7584", IsUnique = true)]
public partial class User
{
    [Key]
    public Guid UserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string DisplayName { get; set; } = null!;

    [StringLength(320)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(5)]
    [Unicode(false)]
    public string? CountryCode { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string ProfileImageUrl { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string HashPassword { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string PasswordSalt { get; set; } = null!;

    [StringLength(512)]
    [Unicode(false)]
    public string? RefreshToken { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TimeZone { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Language { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string TimeFormat { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string DateFormat { get; set; } = null!;

    public Guid? CreatedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    public Guid? DeletedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DeletedDate { get; set; }

    public bool IsDeleted { get; set; }

    public byte RoleId { get; set; }

    public bool IsSignUpUser { get; set; }

    public bool IsEmailVerified { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? SecretKey { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? SecurityKey { get; set; }

    public byte UserStatusId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? SocialAuthId { get; set; }

    [ForeignKey("CreatedUser")]
    [InverseProperty("InverseCreatedUserNavigation")]
    public virtual User? CreatedUserNavigation { get; set; }

    [ForeignKey("DeletedUser")]
    [InverseProperty("InverseDeletedUserNavigation")]
    public virtual User? DeletedUserNavigation { get; set; }

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<EmailMessage> EmailMessageCreatedUserNavigations { get; set; } = new List<EmailMessage>();

    [InverseProperty("UpdatedUserNavigation")]
    public virtual ICollection<EmailMessage> EmailMessageUpdatedUserNavigations { get; set; } = new List<EmailMessage>();

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<User> InverseCreatedUserNavigation { get; set; } = new List<User>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<User> InverseDeletedUserNavigation { get; set; } = new List<User>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<User> InverseModifiedUserNavigation { get; set; } = new List<User>();

    [ForeignKey("ModifiedUser")]
    [InverseProperty("InverseModifiedUserNavigation")]
    public virtual User? ModifiedUserNavigation { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual UserRole Role { get; set; } = null!;

    [ForeignKey("UserStatusId")]
    [InverseProperty("Users")]
    public virtual UserStatus UserStatus { get; set; } = null!;
}
