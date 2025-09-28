using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIA.Client.API.DTO;

[Index("OrganizationGuid", Name = "UQ__Organiza__C8AFC2B10B1FD96D", IsUnique = true)]
public partial class Organization
{
    [Key]
    public int OrganizationId { get; set; }

    [Column("OrganizationGUID")]
    public Guid OrganizationGuid { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string OrganizationName { get; set; } = null!;

    public int OrganizationSize { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ContactPerson { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? Email { get; set; }

    public long? CreatedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public long? ModifiedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    public long? DeletedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    public byte SubscriptionId { get; set; }

    public bool IsBusiness { get; set; }

    public bool IsEmailVerified { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? EmailVerificationToken { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EmailVerificationTokenTime { get; set; }

    public byte OrganizationStatusId { get; set; }

    [ForeignKey("DeletedUser")]
    [InverseProperty("OrganizationDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [ForeignKey("ModifiedUser")]
    [InverseProperty("OrganizationModifiedUserNavigations")]
    public virtual User? ModifiedUserNavigation { get; set; }

    [ForeignKey("OrganizationStatusId")]
    [InverseProperty("Organizations")]
    public virtual OrganizationStatus OrganizationStatus { get; set; } = null!;

    [ForeignKey("SubscriptionId")]
    [InverseProperty("Organizations")]
    public virtual Subscription Subscription { get; set; } = null!;

    [InverseProperty("Organization")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
