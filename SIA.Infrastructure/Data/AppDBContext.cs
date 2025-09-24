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

    public virtual DbSet<EmailMessage> EmailMessages { get; set; }

    public virtual DbSet<EmailServer> EmailServers { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<SiatimeZone> SiatimeZones { get; set; }

    public virtual DbSet<SuperUser> SuperUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailMessage>(entity =>
        {
            entity.HasKey(e => e.EmailMessageId).HasName("PK__EmailMes__2F4E92AEECA9DB35");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.EmailMessageCreatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmailMess__Creat__787EE5A0");

            entity.HasOne(d => d.UpdatedUserNavigation).WithMany(p => p.EmailMessageUpdatedUserNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmailMess__Updat__7A672E12");
        });

        modelBuilder.Entity<EmailServer>(entity =>
        {
            entity.HasKey(e => e.EmailSmtpHost).HasName("PK__EmailSer__2860E5FCCE680BD5");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageCode).HasName("PK__Language__8B8C8A3548620497");
        });

        modelBuilder.Entity<SiatimeZone>(entity =>
        {
            entity.HasKey(e => e.TimeZoneName).HasName("PK__TimeZone__7043C63F3B3CE8EE");
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
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CA1341082");

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateFormat).HasDefaultValue("YYYY-MM-DD");
            entity.Property(e => e.DeletedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Language).HasDefaultValue("en");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TimeFormat).HasDefaultValue("24H");
            entity.Property(e => e.TimeZone).HasDefaultValue("UTC");

            entity.HasOne(d => d.CreatedUserNavigation).WithMany(p => p.InverseCreatedUserNavigation).HasConstraintName("FK__Users__CreatedUs__693CA210");

            entity.HasOne(d => d.DeletedUserNavigation).WithMany(p => p.InverseDeletedUserNavigation).HasConstraintName("FK__Users__DeletedUs__6D0D32F4");

            entity.HasOne(d => d.ModifiedUserNavigation).WithMany(p => p.InverseModifiedUserNavigation).HasConstraintName("FK__Users__ModifiedU__6B24EA82");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__6FE99F9F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
