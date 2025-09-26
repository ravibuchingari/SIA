using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("EmailSubject", Name = "UQ__EmailMes__31F4E1A7352AEA18", IsUnique = true)]
public partial class EmailMessage
{
    [Key]
    [StringLength(30)]
    [Unicode(false)]
    public string EmailMessageId { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string EmailSubject { get; set; } = null!;

    [StringLength(4000)]
    public string EmailBody { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }
}
