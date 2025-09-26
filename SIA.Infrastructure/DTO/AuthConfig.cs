using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIA.Infrastructure.DTO;

[Table("AuthConfig")]
public partial class AuthConfig
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string AuthProvider { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? ClientId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? SecretKey { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Authority { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? TenantId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? RedirectUrl { get; set; }

    public bool IsActive { get; set; }

    [Column("UserInfoAPI")]
    [StringLength(250)]
    [Unicode(false)]
    public string? UserInfoApi { get; set; }
}
