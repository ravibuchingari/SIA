using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("tbl_user_log")]
public partial class TblUserLog
{
    [Key]
    public int UserLogId { get; set; }

    public int UserRowId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LogDate { get; set; }

    [ForeignKey("UserRowId")]
    [InverseProperty("TblUserLogs")]
    public virtual TblUser UserRow { get; set; } = null!;
}
