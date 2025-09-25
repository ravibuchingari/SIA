using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIA.Infrastructure.DTO;

[Index("UserId", Name = "UQ__Users__1788CCAD2A2F2F49", IsUnique = true)]
public partial class SuperUser
{
    [Key]
    [Column("UserRowID")]
    public int UserRowId { get; set; }

    [Column("UserID")]
    [StringLength(150)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string UserSaltKey { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Mobile { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string AccessToken { get; set; } = null!;

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("InverseCreatedByNavigation")]
    public virtual SuperUser? CreatedByNavigation { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<SuperUser> InverseCreatedByNavigation { get; set; } = new List<SuperUser>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<SuperUser> InverseUpdatedByNavigation { get; set; } = new List<SuperUser>();

    [ForeignKey("UpdatedBy")]
    [InverseProperty("InverseUpdatedByNavigation")]
    public virtual SuperUser? UpdatedByNavigation { get; set; }
}
