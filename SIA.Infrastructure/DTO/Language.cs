using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("LanguageType", Name = "UQ__Language__8B12195FDEA58B74", IsUnique = true)]
public partial class Language
{
    [Key]
    [StringLength(10)]
    [Unicode(false)]
    public string LanguageCode { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string LanguageType { get; set; } = null!;
}
