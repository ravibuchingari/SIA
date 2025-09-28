using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

public partial class RefreshToken
{
    [Key]
    public long UserId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Token { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime Expires { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Created { get; set; }
}
