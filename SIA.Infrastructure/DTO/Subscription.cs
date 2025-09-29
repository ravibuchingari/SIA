using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("SubscriptionName", Name = "UQ__Subcript__0976646B5D95A705", IsUnique = true)]
public partial class Subscription
{
    [Key]
    public byte SubscriptionId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string SubscriptionName { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [StringLength(512)]
    [Unicode(false)]
    public string? SubscriptionDescription { get; set; }

    public int MaxMeetings { get; set; }

    [Column("AIHours")]
    public int Aihours { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string SupportLevel { get; set; } = null!;

    [StringLength(512)]
    [Unicode(false)]
    public string BillingCycleOptions { get; set; } = null!;

    public int TrialPeriodDays { get; set; }

    [StringLength(512)]
    [Unicode(false)]
    public string? AddOns { get; set; }

    [StringLength(512)]
    [Unicode(false)]
    public string? FeatureFlags { get; set; }

    [InverseProperty("Subscription")]
    public virtual ICollection<BillingHistory> BillingHistories { get; set; } = new List<BillingHistory>();

    [InverseProperty("Subscription")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Subscription")]
    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    [InverseProperty("Subscription")]
    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
