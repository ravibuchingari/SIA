using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("SessionToken", Name = "UQ__UserSess__46BDD124C27122CF", IsUnique = true)]
[Index("UserSessionGuid", Name = "UQ__UserSess__9574EDBFECA8CB0A", IsUnique = true)]
public partial class UserSession
{
    [Key]
    public long UserSessionId { get; set; }

    [Column("UserSessionGUID")]
    public Guid UserSessionGuid { get; set; }

    public long UserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DeviceName { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string DeviceType { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? OperatingSystem { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? GeographicLocation { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Country { get; set; }

    [StringLength(512)]
    [Unicode(false)]
    public string SessionToken { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime LoginTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastActivity { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string SessionStatus { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? LogoutTime { get; set; }

    public long CreatedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public long ModifiedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    public long? DeletedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDate { get; set; }

    [ForeignKey("CreatedUser")]
    [InverseProperty("UserSessionCreatedUserNavigations")]
    public virtual User CreatedUserNavigation { get; set; } = null!;

    [ForeignKey("DeletedUser")]
    [InverseProperty("UserSessionDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [ForeignKey("ModifiedUser")]
    [InverseProperty("UserSessionModifiedUserNavigations")]
    public virtual User ModifiedUserNavigation { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserSessionUsers")]
    public virtual User User { get; set; } = null!;
}
