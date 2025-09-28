using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIA.Client.API.DTO;

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

    [StringLength(100)]
    [Unicode(false)]
    public string EmailDisplayName { get; set; } = null!;
}
