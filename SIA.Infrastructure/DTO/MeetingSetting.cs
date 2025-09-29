using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

public partial class MeetingSetting
{
    [Key]
    public long EventId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string MeetingDriver { get; set; } = null!;

    public bool ApplyToRecurring { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string MeetingType { get; set; } = null!;

    public bool IsVideoMandatory { get; set; }

    [StringLength(4000)]
    public string? Agenda { get; set; }

    [StringLength(4000)]
    public string? AgendaMyBrief { get; set; }

    [StringLength(4000)]
    public string? AgendaParticipantBrief { get; set; }

    [StringLength(4000)]
    public string? AgendaSuggestions { get; set; }

    [StringLength(4000)]
    public string? ExpectedOutcome { get; set; }

    [StringLength(4000)]
    public string? OutcomeMyBrief { get; set; }

    [StringLength(4000)]
    public string? OutcomeParticipantBrief { get; set; }

    [StringLength(4000)]
    public string? OutcomeSuggestions { get; set; }

    public bool SendPreMeetingNotifications { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string NotificationParticipantScope { get; set; } = null!;

    [StringLength(4000)]
    public string? NotificationDataQuery { get; set; }

    public TimeOnly? NotificationTime { get; set; }

    [Column("IsSIANotesEnabled")]
    public bool IsSianotesEnabled { get; set; }

    [Column("SIANoteTakingLevel")]
    [StringLength(20)]
    [Unicode(false)]
    public string SianoteTakingLevel { get; set; } = null!;

    [Column("SIANoteContentType")]
    [StringLength(20)]
    [Unicode(false)]
    public string SianoteContentType { get; set; } = null!;

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
    [InverseProperty("MeetingSettingCreatedUserNavigations")]
    public virtual User CreatedUserNavigation { get; set; } = null!;

    [ForeignKey("DeletedUser")]
    [InverseProperty("MeetingSettingDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("MeetingSetting")]
    public virtual CalendarEvent Event { get; set; } = null!;

    [ForeignKey("ModifiedUser")]
    [InverseProperty("MeetingSettingModifiedUserNavigations")]
    public virtual User ModifiedUserNavigation { get; set; } = null!;
}
