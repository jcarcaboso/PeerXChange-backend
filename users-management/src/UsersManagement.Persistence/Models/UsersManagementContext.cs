using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UsersManagement.Persistence.Extensions;

namespace UsersManagement.Persistence.Models;

public partial class UsersManagementContext : DbContext
{
    public UsersManagementContext()
    {
    }

    public UsersManagementContext(DbContextOptions<UsersManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schemaversion> Schemaversions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserConfiguration> UserConfigurations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role", "dbo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Schemaversion>(entity =>
        {
            entity.HasKey(e => e.Schemaversionsid).HasName("PK_schemaversions_Id");

            entity.ToTable("schemaversions");

            entity.Property(e => e.Schemaversionsid).HasColumnName("schemaversionsid");
            entity.Property(e => e.Applied)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("applied");
            entity.Property(e => e.Scriptname)
                .HasMaxLength(255)
                .HasColumnName("scriptname");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Wallet).HasName("user_pkey");

            entity.ToTable("user", "dbo");

            entity.Property(e => e.Wallet).HasColumnName("wallet");
            entity.Property(e => e.CreationTimestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_timestamp");
            entity.Property(e => e.DeleteDeadline).HasColumnName("delete_deadline");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Language)
                .HasMaxLength(2)
                .HasColumnName("language");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role");
        });

        modelBuilder.Entity<UserConfiguration>(entity =>
        {
            entity.HasKey(e => e.UserWallet).HasName("user_configuration_pkey");

            entity.ToTable("user_configuration", "dbo");

            entity.Property(e => e.UserWallet).HasColumnName("user_wallet");
            entity.Property(e => e.DefaultCurrency)
                .HasMaxLength(3)
                .HasColumnName("default_currency");
            entity.Property(e => e.Email).HasColumnName("email");

            entity.HasOne(d => d.UserWalletNavigation).WithOne(p => p.UserConfiguration)
                .HasForeignKey<UserConfiguration>(d => d.UserWallet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_address");
        });
        
        modelBuilder.AddOutboxPattern();
    }
}
