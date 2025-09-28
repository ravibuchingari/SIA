using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("EmailServer")]
public partial class EmailServer
{
    [Key]
    [StringLength(150)]
    [Unicode(false)]
    public string EmailSmtpHost { get; set; } = null!;

    public int EmailPort { get; set; }

    [Column("EmailSSLEnabled")]
    public bool EmailSslenabled { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string EmailUserId { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string EmailPassword { get; set; } = null!;

    public bool IsActive { get; set; }
}
