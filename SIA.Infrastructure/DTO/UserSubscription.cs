using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

public partial class UserSubscription
{
    [Key]
    public int UserSubscriptionId { get; set; }

    public long UserId { get; set; }

    public byte SubscriptionId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string BillingCycle { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NextRenewal { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TrialEndDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string RenewalType { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? GracePeriodEnd { get; set; }

    public int RetryAttempts { get; set; }

    [StringLength(4000)]
    public string? CancellationReason { get; set; }

    public byte? ParentSubscriptionId { get; set; }

    public bool ProrationApplied { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string Currency { get; set; } = null!;

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
    [InverseProperty("UserSubscriptionCreatedUserNavigations")]
    public virtual User CreatedUserNavigation { get; set; } = null!;

    [ForeignKey("DeletedUser")]
    [InverseProperty("UserSubscriptionDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [ForeignKey("ModifiedUser")]
    [InverseProperty("UserSubscriptionModifiedUserNavigations")]
    public virtual User ModifiedUserNavigation { get; set; } = null!;

    [ForeignKey("SubscriptionId")]
    [InverseProperty("UserSubscriptions")]
    public virtual Subscription Subscription { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserSubscriptionUsers")]
    public virtual User User { get; set; } = null!;
}
