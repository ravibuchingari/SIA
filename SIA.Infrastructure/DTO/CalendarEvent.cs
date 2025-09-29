using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("UserId", "UserAccountId", "ProviderEventId", Name = "IX_CalendarEvents", IsUnique = true)]
public partial class CalendarEvent
{
    [Key]
    public long EventId { get; set; }

    public long UserId { get; set; }

    public int UserAccountId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string ProviderEventId { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? EventTitle { get; set; }

    [StringLength(4000)]
    [Unicode(false)]
    public string? EventDescription { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? EventLocation { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndTime { get; set; }

    [StringLength(512)]
    [Unicode(false)]
    public string? MeetingUrl { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? MeetingPasscode { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? MeetingId { get; set; }

    [StringLength(4000)]
    [Unicode(false)]
    public string? DialInInfo { get; set; }

    public long CreatedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public long ModifiedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    public long? DeletedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDate { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("CreatedUser")]
    [InverseProperty("CalendarEventCreatedUserNavigations")]
    public virtual User CreatedUserNavigation { get; set; } = null!;

    [ForeignKey("DeletedUser")]
    [InverseProperty("CalendarEventDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [InverseProperty("Event")]
    public virtual ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();

    [InverseProperty("Event")]
    public virtual MeetingSetting? MeetingSetting { get; set; }

    [InverseProperty("Event")]
    public virtual MeetingSummary? MeetingSummary { get; set; }

    [ForeignKey("ModifiedUser")]
    [InverseProperty("CalendarEventModifiedUserNavigations")]
    public virtual User ModifiedUserNavigation { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("CalendarEventUsers")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("UserAccountId")]
    [InverseProperty("CalendarEvents")]
    public virtual UserAccount UserAccount { get; set; } = null!;
}
