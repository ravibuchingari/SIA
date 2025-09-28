using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIA.Client.API.DTO;

[Index("Username", Name = "UQ__Users__536C85E49974E454", IsUnique = true)]
[Index("UserGuid", Name = "UQ__Users__81B7740DAB55A705", IsUnique = true)]
[Index("Email", Name = "UQ__Users__A9D1053460ECB39C", IsUnique = true)]
public partial class User
{
    [Key]
    public long UserId { get; set; }

    [Column("UserGUID")]
    public Guid UserGuid { get; set; }

    public int OrganizationId { get; set; }

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
    public string RefreshToken { get; set; } = null!;

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

    public byte RoleId { get; set; }

    public bool IsSignUpUser { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string SecretKey { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string? SecurityKey { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? SocialAuthId { get; set; }

    public long? CreatedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public long? ModifiedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    public long? DeletedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DeletedDate { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("CreatedUser")]
    [InverseProperty("InverseCreatedUserNavigation")]
    public virtual User? CreatedUserNavigation { get; set; }

    [ForeignKey("DeletedUser")]
    [InverseProperty("InverseDeletedUserNavigation")]
    public virtual User? DeletedUserNavigation { get; set; }

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<User> InverseCreatedUserNavigation { get; set; } = new List<User>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<User> InverseDeletedUserNavigation { get; set; } = new List<User>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<User> InverseModifiedUserNavigation { get; set; } = new List<User>();

    [ForeignKey("ModifiedUser")]
    [InverseProperty("InverseModifiedUserNavigation")]
    public virtual User? ModifiedUserNavigation { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("Users")]
    public virtual Organization Organization { get; set; } = null!;

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<Organization> OrganizationDeletedUserNavigations { get; set; } = new List<Organization>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<Organization> OrganizationModifiedUserNavigations { get; set; } = new List<Organization>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual UserRole Role { get; set; } = null!;
}
