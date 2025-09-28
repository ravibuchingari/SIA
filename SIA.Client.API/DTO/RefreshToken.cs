using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIA.Client.API.DTO;

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
