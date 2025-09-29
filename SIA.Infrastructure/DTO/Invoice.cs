using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("Invoice")]
[Index("InvoiceNumber", Name = "UQ__Invoice__D776E981A09A216E", IsUnique = true)]
public partial class Invoice
{
    [Key]
    public int InvoiceId { get; set; }

    public byte SubscriptionId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string InvoiceNumber { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string InvoiceStatus { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime IssueDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DueDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal DiscountAmount { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string Currency { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? DownloadUrl { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ProviderInvoiceId { get; set; }

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

    [InverseProperty("Invoice")]
    public virtual ICollection<BillingHistory> BillingHistories { get; set; } = new List<BillingHistory>();

    [ForeignKey("CreatedUser")]
    [InverseProperty("InvoiceCreatedUserNavigations")]
    public virtual User CreatedUserNavigation { get; set; } = null!;

    [ForeignKey("DeletedUser")]
    [InverseProperty("InvoiceDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [ForeignKey("ModifiedUser")]
    [InverseProperty("InvoiceModifiedUserNavigations")]
    public virtual User ModifiedUserNavigation { get; set; } = null!;

    [ForeignKey("SubscriptionId")]
    [InverseProperty("Invoices")]
    public virtual Subscription Subscription { get; set; } = null!;
}
