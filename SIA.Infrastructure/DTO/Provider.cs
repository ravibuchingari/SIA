using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("ProviderName", Name = "UQ__Provider__7D057CE5BA8BF29F", IsUnique = true)]
public partial class Provider
{
    [Key]
    public byte ProviderId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string ProviderName { get; set; } = null!;

    public bool IsActive { get; set; }

    [InverseProperty("Provider")]
    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
