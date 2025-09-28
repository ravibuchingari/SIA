using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("SubscriptionName", Name = "UQ__Subcript__0976646B5D95A705", IsUnique = true)]
public partial class Subscription
{
    [Key]
    public byte SubscriptionId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string SubscriptionName { get; set; } = null!;

    [InverseProperty("Subscription")]
    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();
}
