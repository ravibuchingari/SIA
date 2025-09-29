using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SIA.Infrastructure.DTO;

[Index("Username", Name = "UQ__Users__536C85E49974E454", IsUnique = true)]
[Index("UserGuid", Name = "UQ__Users__81B7740DAB55A705", IsUnique = true)]
[Index("Email", Name = "UQ__Users__A9D1053460ECB39C", IsUnique = true)]
public partial class User
{
    [Key]
    public long UserId { get; set; }

    [Column("UserGUID")]
    public Guid UserGuid { get; set; }

    public int OrganizationId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string DisplayName { get; set; } = null!;

    [StringLength(320)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(5)]
    [Unicode(false)]
    public string? CountryCode { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string ProfileImageUrl { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string HashPassword { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string PasswordSalt { get; set; } = null!;

    [StringLength(512)]
    [Unicode(false)]
    public string RefreshToken { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string TimeZone { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Language { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string TimeFormat { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string DateFormat { get; set; } = null!;

    public byte RoleId { get; set; }

    public bool IsSignUpUser { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string SecretKey { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string? SecurityKey { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? SocialAuthId { get; set; }

    public long? CreatedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public long? ModifiedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    public long? DeletedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string UserStatus { get; set; } = null!;

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<BillingHistory> BillingHistoryCreatedUserNavigations { get; set; } = new List<BillingHistory>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<BillingHistory> BillingHistoryDeletedUserNavigations { get; set; } = new List<BillingHistory>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<BillingHistory> BillingHistoryModifiedUserNavigations { get; set; } = new List<BillingHistory>();

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<CalendarEvent> CalendarEventCreatedUserNavigations { get; set; } = new List<CalendarEvent>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<CalendarEvent> CalendarEventDeletedUserNavigations { get; set; } = new List<CalendarEvent>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<CalendarEvent> CalendarEventModifiedUserNavigations { get; set; } = new List<CalendarEvent>();

    [InverseProperty("User")]
    public virtual ICollection<CalendarEvent> CalendarEventUsers { get; set; } = new List<CalendarEvent>();

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<Coupon> CouponCreatedUserNavigations { get; set; } = new List<Coupon>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<Coupon> CouponDeletedUserNavigations { get; set; } = new List<Coupon>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<Coupon> CouponModifiedUserNavigations { get; set; } = new List<Coupon>();

    [ForeignKey("CreatedUser")]
    [InverseProperty("InverseCreatedUserNavigation")]
    public virtual User? CreatedUserNavigation { get; set; }

    [ForeignKey("DeletedUser")]
    [InverseProperty("InverseDeletedUserNavigation")]
    public virtual User? DeletedUserNavigation { get; set; }

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<EventParticipant> EventParticipantCreatedUserNavigations { get; set; } = new List<EventParticipant>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<EventParticipant> EventParticipantDeletedUserNavigations { get; set; } = new List<EventParticipant>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<EventParticipant> EventParticipantModifiedUserNavigations { get; set; } = new List<EventParticipant>();

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<User> InverseCreatedUserNavigation { get; set; } = new List<User>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<User> InverseDeletedUserNavigation { get; set; } = new List<User>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<User> InverseModifiedUserNavigation { get; set; } = new List<User>();

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<Invoice> InvoiceCreatedUserNavigations { get; set; } = new List<Invoice>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<Invoice> InvoiceDeletedUserNavigations { get; set; } = new List<Invoice>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<Invoice> InvoiceModifiedUserNavigations { get; set; } = new List<Invoice>();

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<MeetingSetting> MeetingSettingCreatedUserNavigations { get; set; } = new List<MeetingSetting>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<MeetingSetting> MeetingSettingDeletedUserNavigations { get; set; } = new List<MeetingSetting>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<MeetingSetting> MeetingSettingModifiedUserNavigations { get; set; } = new List<MeetingSetting>();

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<MeetingSummary> MeetingSummaryCreatedUserNavigations { get; set; } = new List<MeetingSummary>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<MeetingSummary> MeetingSummaryDeletedUserNavigations { get; set; } = new List<MeetingSummary>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<MeetingSummary> MeetingSummaryModifiedUserNavigations { get; set; } = new List<MeetingSummary>();

    [ForeignKey("ModifiedUser")]
    [InverseProperty("InverseModifiedUserNavigation")]
    public virtual User? ModifiedUserNavigation { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("Users")]
    public virtual Organization Organization { get; set; } = null!;

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<Organization> OrganizationDeletedUserNavigations { get; set; } = new List<Organization>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<Organization> OrganizationModifiedUserNavigations { get; set; } = new List<Organization>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual UserRole Role { get; set; } = null!;

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<UserAccount> UserAccountCreatedUserNavigations { get; set; } = new List<UserAccount>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<UserAccount> UserAccountDeletedUserNavigations { get; set; } = new List<UserAccount>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<UserAccount> UserAccountModifiedUserNavigations { get; set; } = new List<UserAccount>();

    [InverseProperty("User")]
    public virtual ICollection<UserAccount> UserAccountUsers { get; set; } = new List<UserAccount>();

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<UserSession> UserSessionCreatedUserNavigations { get; set; } = new List<UserSession>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<UserSession> UserSessionDeletedUserNavigations { get; set; } = new List<UserSession>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<UserSession> UserSessionModifiedUserNavigations { get; set; } = new List<UserSession>();

    [InverseProperty("User")]
    public virtual ICollection<UserSession> UserSessionUsers { get; set; } = new List<UserSession>();

    [InverseProperty("CreatedUserNavigation")]
    public virtual ICollection<UserSubscription> UserSubscriptionCreatedUserNavigations { get; set; } = new List<UserSubscription>();

    [InverseProperty("DeletedUserNavigation")]
    public virtual ICollection<UserSubscription> UserSubscriptionDeletedUserNavigations { get; set; } = new List<UserSubscription>();

    [InverseProperty("ModifiedUserNavigation")]
    public virtual ICollection<UserSubscription> UserSubscriptionModifiedUserNavigations { get; set; } = new List<UserSubscription>();

    [InverseProperty("User")]
    public virtual ICollection<UserSubscription> UserSubscriptionUsers { get; set; } = new List<UserSubscription>();
}
