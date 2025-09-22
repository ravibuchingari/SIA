using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("tbl_transactions")]
public partial class TblTransaction
{
    [Key]
    public int TransactionRowId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime TransactionDate { get; set; }

    public int CategoryId { get; set; }

    public int LedgerRowId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Credit { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Debit { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Inwards { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Outwards { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Remarks { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("TblTransactions")]
    public virtual TblCategory Category { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("TblTransactionCreatedByNavigations")]
    public virtual TblUser CreatedByNavigation { get; set; } = null!;

    [ForeignKey("LedgerRowId")]
    [InverseProperty("TblTransactions")]
    public virtual TblLedger LedgerRow { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("TblTransactionUpdatedByNavigations")]
    public virtual TblUser UpdatedByNavigation { get; set; } = null!;
}
