using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("CouponGuid", Name = "UQ__Coupons__3C37468FAD91B072", IsUnique = true)]
[Index("Code", Name = "UQ__Coupons__A25C5AA796BECD4B", IsUnique = true)]
public partial class Coupon
{
    [Key]
    public int CouponId { get; set; }

    [Column("CouponGUID")]
    public Guid CouponGuid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string DiscountType { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal DiscountValue { get; set; }

    public int? MaxRedemptions { get; set; }

    public int? CurrentRedemptions { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryDate { get; set; }

    public long CreatedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public long ModifiedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    public long? DeletedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDate { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("CreatedUser")]
    [InverseProperty("CouponCreatedUserNavigations")]
    public virtual User CreatedUserNavigation { get; set; } = null!;

    [ForeignKey("DeletedUser")]
    [InverseProperty("CouponDeletedUserNavigations")]
    public virtual User? DeletedUserNavigation { get; set; }

    [ForeignKey("ModifiedUser")]
    [InverseProperty("CouponModifiedUserNavigations")]
    public virtual User ModifiedUserNavigation { get; set; } = null!;
}
