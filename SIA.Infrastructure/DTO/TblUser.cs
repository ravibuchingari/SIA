using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("tbl_users")]
[Index("UserId", Name = "IX_TBL_USERS", IsUnique = true)]
public partial class TblUser
{
    [Key]
    public int UserRowId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    public bool IsAdmin { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<TblTransaction> TblTransactionCreatedByNavigations { get; set; } = new List<TblTransaction>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<TblTransaction> TblTransactionUpdatedByNavigations { get; set; } = new List<TblTransaction>();

    [InverseProperty("UserRow")]
    public virtual ICollection<TblUserLog> TblUserLogs { get; set; } = new List<TblUserLog>();
}
