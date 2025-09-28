using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("SIATimeZones")]
public partial class SiatimeZone
{
    [Key]
    [StringLength(100)]
    [Unicode(false)]
    public string TimeZoneName { get; set; } = null!;

    [Column("UTCOffset")]
    [StringLength(10)]
    [Unicode(false)]
    public string Utcoffset { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string CommonRegions { get; set; } = null!;
}
