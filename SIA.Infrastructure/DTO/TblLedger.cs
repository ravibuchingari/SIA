using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("tbl_ledgers")]
public partial class TblLedger
{
    [Key]
    public int LedgerRowId { get; set; }

    public int CategoryId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string GroupName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string LedgerName { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Address { get; set; }

    public int? CityId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ZipCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? EmailAddress { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Credit { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Debit { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Inwards { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Outwards { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("TblLedgers")]
    public virtual TblCategory Category { get; set; } = null!;

    [InverseProperty("LedgerRow")]
    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
}
