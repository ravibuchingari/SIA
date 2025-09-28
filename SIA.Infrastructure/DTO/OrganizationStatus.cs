using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("OrganizationStatus")]
[Index("OrganizationStatusName", Name = "UQ__Organiza__6605B0610926E443", IsUnique = true)]
public partial class OrganizationStatus
{
    [Key]
    public byte OrganizationStatusId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string OrganizationStatusName { get; set; } = null!;

    [InverseProperty("OrganizationStatus")]
    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();
}
