using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("EventId", "Email", Name = "IX_EventParticipants", IsUnique = true)]
[Index("EventParticipantGuid", Name = "UQ__EventPar__FDD24019CE684BE8", IsUnique = true)]
public partial class EventParticipant
{
    [Key]
    public long EventParticipantId { get; set; }

    [Column("EventParticipantGUID")]
    public Guid EventParticipantGuid { get; set; }

    public long EventId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? DisplayName { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string ParticipantStatus { get; set; } = null!;

    public bool IsOrganizer { get; set; }

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
    [InverseProperty("EventParticipantCreatedUserNavigations")]
    public virtual User CreatedUserNavigation { get; set; } = null!;

    [ForeignKey("DeletedUser")]
    [InverseProperty("EventParticipantDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("EventParticipants")]
    public virtual CalendarEvent Event { get; set; } = null!;

    [ForeignKey("ModifiedUser")]
    [InverseProperty("EventParticipantModifiedUserNavigations")]
    public virtual User ModifiedUserNavigation { get; set; } = null!;
}
