using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("tbl_settings")]
public partial class TblSetting
{
    [Key]
    [StringLength(100)]
    [Unicode(false)]
    public string CompanyName { get; set; } = null!;

    public DateOnly FinancialYearFrom { get; set; }

    public DateOnly FinancialYearTo { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? FromEmail { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? ToEmail { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? EmailUserId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? EmailPassword { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? EmailSmtp { get; set; }

    public int? EmailSmtpPort { get; set; }

    [Column("EmailSSLRequired")]
    public bool? EmailSslrequired { get; set; }
}
