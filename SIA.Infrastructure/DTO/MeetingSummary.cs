using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

public partial class MeetingSummary
{
    [Key]
    public long EventId { get; set; }

    [Unicode(false)]
    public string SummaryData { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? GeneratorVersion { get; set; }

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
    [InverseProperty("MeetingSummaryCreatedUserNavigations")]
    public virtual User CreatedUserNavigation { get; set; } = null!;

    [ForeignKey("DeletedUser")]
    [InverseProperty("MeetingSummaryDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("MeetingSummary")]
    public virtual CalendarEvent Event { get; set; } = null!;

    [ForeignKey("ModifiedUser")]
    [InverseProperty("MeetingSummaryModifiedUserNavigations")]
    public virtual User ModifiedUserNavigation { get; set; } = null!;
}
