using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("UserAccountGuid", Name = "UQ__UserAcco__F4C4BD521EC702BE", IsUnique = true)]
public partial class UserAccount
{
    [Key]
    public int UserAccountId { get; set; }

    [Column("UserAccountGUID")]
    public Guid UserAccountGuid { get; set; }

    public long UserId { get; set; }

    public byte ProviderId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string ProviderAccountId { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string HashPassword { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string CalendaIntegratedStatus { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string UserAccountStatus { get; set; } = null!;

    public long CreatedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public long ModifiedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    public long? DeletedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDate { get; set; }

    [InverseProperty("UserAccount")]
    public virtual ICollection<CalendarEvent> CalendarEvents { get; set; } = new List<CalendarEvent>();

    [ForeignKey("CreatedUser")]
    [InverseProperty("UserAccountCreatedUserNavigations")]
    public virtual User CreatedUserNavigation { get; set; } = null!;

    [ForeignKey("DeletedUser")]
    [InverseProperty("UserAccountDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [ForeignKey("ModifiedUser")]
    [InverseProperty("UserAccountModifiedUserNavigations")]
    public virtual User ModifiedUserNavigation { get; set; } = null!;

    [ForeignKey("ProviderId")]
    [InverseProperty("UserAccounts")]
    public virtual Provider Provider { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserAccountUsers")]
    public virtual User User { get; set; } = null!;
}
