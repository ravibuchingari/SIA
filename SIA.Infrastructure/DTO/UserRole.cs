using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIA.Infrastructure.DTO;

[Table("UserRole")]
[Index("RoleName", Name = "UQ__UserRole__8A2B616065EBE9FD", IsUnique = true)]
public partial class UserRole
{
    [Key]
    public byte RoleId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
