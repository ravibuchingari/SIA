using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SIA.Infrastructure.DTO;

namespace SIA.Infrastructure.Data;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BillingHistory> BillingHistories { get; set; }

    public virtual DbSet<CalendarEvent> CalendarEvents { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<EmailMessage> EmailMessages { get; set; }

    public virtual DbSet<EmailServer> EmailServers { get; set; }

    public virtual DbSet<EventParticipant> EventParticipants { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<MeetingSetting> MeetingSettings { get; set; }

    public virtual DbSet<MeetingSummary> MeetingSummaries { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationStatus> OrganizationStatuses { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<SiatimeZone> SiatimeZones { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SuperUser> SuperUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BillingHistory>(entity =>
        {
            entity.HasKey(e => e.BillingId).HasName("PK__BillingH__F1656DF3FD09B7F6");

            entity.Property(e => e.BillingStatus).HasDefaultValue("Pending");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Currency).HasDefaultValue("USD");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.BillingHistoryCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillingHi__Creat__52AE4273");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.BillingHistoryDeletedUserNavigations).HasConstraintName("FK__BillingHi__Delet__567ED357");

            entity.HasOne(d => d.Invoice).WithMany(p => p.BillingHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillingHi__Invoi__4C0144E4");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.BillingHistoryModifiedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillingHi__Modif__54968AE5");

            entity.HasOne(d => d.Subscription).WithMany(p => p.BillingHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillingHi__Subsc__4B0D20AB");
        });

        modelBuilder.Entity<CalendarEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Calendar__7944C810E5762953");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.CalendarEventCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CalendarE__Creat__7AF13DF7");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.CalendarEventDeletedUserNavigations).HasConstraintName("FK__CalendarE__Delet__7EC1CEDB");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.CalendarEventModifiedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CalendarE__Modif__7CD98669");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.CalendarEvents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CalendarE__UserA__79FD19BE");

            entity.HasOne(d => d.User).WithMany(p => p.CalendarEventUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CalendarE__UserI__7908F585");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponId).HasName("PK__Coupons__384AF1BAD551883C");

            entity.Property(e => e.CouponGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CurrentRedemptions).HasDefaultValue(0);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.CouponCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coupons__Created__66B53B20");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.CouponDeletedUserNavigations).HasConstraintName("FK__Coupons__Deleted__6A85CC04");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.CouponModifiedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coupons__Modifie__689D8392");
        });

        modelBuilder.Entity<EmailMessage>(entity =>
        {
            entity.HasKey(e => e.EmailMessageId).HasName("PK__EmailMes__2F4E92AEECA9DB35");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EmailDisplayName).HasDefaultValue("SIA");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<EmailServer>(entity =>
        {
            entity.HasKey(e => e.EmailSmtpHost).HasName("PK__EmailSer__2860E5FCCE680BD5");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<EventParticipant>(entity =>
        {
            entity.HasKey(e => e.EventParticipantId).HasName("PK__EventPar__09F32B92F2D2FB44");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EventParticipantGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ParticipantStatus).HasDefaultValue("NeedsAction");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.EventParticipantCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EventPart__Creat__084B3915");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.EventParticipantDeletedUserNavigations).HasConstraintName("FK__EventPart__Delet__0C1BC9F9");

            entity.HasOne(d => d.Event).WithMany(p => p.EventParticipants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EventPart__Event__047AA831");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.EventParticipantModifiedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EventPart__Modif__0A338187");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__D796AAB5687ECB42");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Currency).HasDefaultValue("USD");
            entity.Property(e => e.InvoiceStatus).HasDefaultValue("Pending");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.InvoiceCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Created__436BFEE3");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.InvoiceDeletedUserNavigations).HasConstraintName("FK__Invoice__Deleted__473C8FC7");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.InvoiceModifiedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Modifie__45544755");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Subscri__3CBF0154");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageCode).HasName("PK__Language__8B8C8A3548620497");
        });

        modelBuilder.Entity<MeetingSetting>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__MeetingS__7944C8103D8E2341");

            entity.Property(e => e.EventId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsSianotesEnabled).HasDefaultValue(true);
            entity.Property(e => e.MeetingType).HasDefaultValue("Hybrid");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NotificationParticipantScope).HasDefaultValue("All");
            entity.Property(e => e.SianoteContentType).HasDefaultValue("All");
            entity.Property(e => e.SianoteTakingLevel).HasDefaultValue("Manual");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.MeetingSettingCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MeetingSe__Creat__1C5231C2");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.MeetingSettingDeletedUserNavigations).HasConstraintName("FK__MeetingSe__Delet__2022C2A6");

            entity.HasOne(d => d.Event).WithOne(p => p.MeetingSetting)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MeetingSe__Event__0EF836A4");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.MeetingSettingModifiedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MeetingSe__Modif__1E3A7A34");
        });

        modelBuilder.Entity<MeetingSummary>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__MeetingS__7944C81019AD737C");

            entity.Property(e => e.EventId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.MeetingSummaryCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MeetingSu__Creat__24E777C3");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.MeetingSummaryDeletedUserNavigations).HasConstraintName("FK__MeetingSu__Delet__28B808A7");

            entity.HasOne(d => d.Event).WithOne(p => p.MeetingSummary)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MeetingSu__Event__23F3538A");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.MeetingSummaryModifiedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MeetingSu__Modif__26CFC035");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK__Organiza__CADB0B1291236B75");

            entity.Property(e => e.EmailVerificationTokenTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.OrganizationGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.OrganizationSize).HasDefaultValue(1);

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.OrganizationDeletedUserNavigations).HasConstraintName("FK_Organizations_Users1");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.OrganizationModifiedUserNavigations).HasConstraintName("FK_Organizations_Users");

            entity.HasOne(d => d.OrganizationStatus).WithMany(p => p.Organizations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizat__Organ__3EDC53F0");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Organizations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizat__Subsc__0C50D423");
        });

        modelBuilder.Entity<OrganizationStatus>(entity =>
        {
            entity.HasKey(e => e.OrganizationStatusId).HasName("PK__Organiza__68BE924FD85FC2B5");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("PK__Provider__B54C687DCBA78C2A");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__RefreshT__1788CC4C54D13308");

            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        modelBuilder.Entity<SiatimeZone>(entity =>
        {
            entity.HasKey(e => e.TimeZoneName).HasName("PK__TimeZone__7043C63F3B3CE8EE");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subcript__9A2B249D48D00E2A");

            entity.Property(e => e.BillingCycleOptions).HasDefaultValue("");
            entity.Property(e => e.SupportLevel).HasDefaultValue("Basic");
        });

        modelBuilder.Entity<SuperUser>(entity =>
        {
            entity.HasKey(e => e.UserRowId).HasName("PK__Users__82B35BF990FABAAB");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation).HasConstraintName("FK__Users__CreatedBy__2D27B809");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation).HasConstraintName("FK__Users__UpdatedBy__2F10007B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C272D498E");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateFormat).HasDefaultValue("YYYY-MM-DD");
            entity.Property(e => e.Language).HasDefaultValue("en");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RefreshToken).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SecretKey).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SecurityKey).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TimeFormat).HasDefaultValue("24H");
            entity.Property(e => e.TimeZone).HasDefaultValue("UTC");
            entity.Property(e => e.UserGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UserStatus).HasDefaultValue("Active");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.InverseCreatedUserNavigation).HasConstraintName("FK__Users__CreatedUs__308E3499");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.InverseDeletedUserNavigation).HasConstraintName("FK__Users__DeletedUs__318258D2");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.InverseModifiedUserNavigation).HasConstraintName("FK__Users__ModifiedU__32767D0B");

            entity.HasOne(d => d.Organization).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Organizat__336AA144");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__345EC57D");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK__UserAcco__DA6C709AF7847A6D");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UserAccountGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UserAccountStatus).HasDefaultValue("Active");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.UserAccountCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAccou__Creat__61316BF4");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.UserAccountDeletedUserNavigations).HasConstraintName("FK__UserAccou__Delet__6225902D");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.UserAccountModifiedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAccou__Modif__6319B466");

            entity.HasOne(d => d.Provider).WithMany(p => p.UserAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAccou__Provi__640DD89F");

            entity.HasOne(d => d.User).WithMany(p => p.UserAccountUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAccou__UserI__6501FCD8");
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.UserSessionId).HasName("PK__UserSess__E73711A50CF7980D");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastActivity).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LoginTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SessionStatus).HasDefaultValue("Active");
            entity.Property(e => e.UserSessionGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.UserSessionCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSessi__Creat__725BF7F6");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.UserSessionDeletedUserNavigations).HasConstraintName("FK__UserSessi__Delet__762C88DA");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.UserSessionModifiedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSessi__Modif__74444068");

            entity.HasOne(d => d.User).WithMany(p => p.UserSessionUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSessi__UserI__6CA31EA0");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.UserSubscriptionId).HasName("PK__UserSubs__D1FD777CCF19EC21");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Currency).HasDefaultValue("USD");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RenewalType).HasDefaultValue("AutoRenew");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.UserSubscriptionCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubsc__Creat__3429BB53");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.UserSubscriptionDeletedUserNavigations).HasConstraintName("FK__UserSubsc__Delet__37FA4C37");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.UserSubscriptionModifiedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubsc__Modif__361203C5");

            entity.HasOne(d => d.Subscription).WithMany(p => p.UserSubscriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubsc__Subsc__2D7CBDC4");

            entity.HasOne(d => d.User).WithMany(p => p.UserSubscriptionUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubsc__UserI__2C88998B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
