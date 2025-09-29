using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("BillingHistory")]
public partial class BillingHistory
{
    [Key]
    public int BillingId { get; set; }

    public byte SubscriptionId { get; set; }

    public int InvoiceId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string BillingStatus { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime BillingDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ProviderTransactionId { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string Currency { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal DiscountAmount { get; set; }

    public int RetryCount { get; set; }

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
    [InverseProperty("BillingHistoryCreatedUserNavigations")]
    public virtual User CreatedUserNavigation { get; set; } = null!;

    [ForeignKey("DeletedUser")]
    [InverseProperty("BillingHistoryDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("BillingHistories")]
    public virtual Invoice Invoice { get; set; } = null!;

    [ForeignKey("ModifiedUser")]
    [InverseProperty("BillingHistoryModifiedUserNavigations")]
    public virtual User ModifiedUserNavigation { get; set; } = null!;

    [ForeignKey("SubscriptionId")]
    [InverseProperty("BillingHistories")]
    public virtual Subscription Subscription { get; set; } = null!;
}
