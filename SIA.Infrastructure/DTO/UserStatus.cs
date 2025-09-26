using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("UserStatus")]
[Index("UserStatusName", Name = "UQ__UserStat__60DF70E8BD3272BA", IsUnique = true)]
public partial class UserStatus
{
    [Key]
    public byte UserStatusId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string UserStatusName { get; set; } = null!;

    [InverseProperty("UserStatus")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
