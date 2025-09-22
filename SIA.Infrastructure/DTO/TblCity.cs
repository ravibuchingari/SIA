using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Table("tbl_cities")]
[Index("CityName", Name = "IX_TBL_CITIES", IsUnique = true)]
public partial class TblCity
{
    [Key]
    public int CityId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CityName { get; set; } = null!;
}
