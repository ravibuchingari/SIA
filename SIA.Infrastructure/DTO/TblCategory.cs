using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("tbl_categories")]
[Index("CategoryName", "GroupName", Name = "IX_tbl_categories", IsUnique = true)]
public partial class TblCategory
{
    [Key]
    public int CategoryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CategoryName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string GroupName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsMaster { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<TblLedger> TblLedgers { get; set; } = new List<TblLedger>();

    [InverseProperty("Category")]
    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
}
