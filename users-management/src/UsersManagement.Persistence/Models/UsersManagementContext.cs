﻿using Microsoft.EntityFrameworkCore;
using UsersManagement.Persistence.Extensions;

namespace UsersManagement.Persistence.Models;

public class UsersManagementContext : DbContext
{
    public UsersManagementContext()
    {
    }

    public UsersManagementContext(DbContextOptions<UsersManagementContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Schemaversion> Schemaversions { get; set; }

    public virtual DbSet<UserAdditionalDatum> UserAdditionalData { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Server=localhost:5432;Database=postgres;User Id=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
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

        modelBuilder.Entity<UserAdditionalDatum>(entity =>
        {
            entity.HasKey(e => e.Wallet).HasName("user_additional_data_pkey");

            entity.ToTable("user_additional_data", "dbo");

            entity.Property(e => e.Wallet).HasColumnName("wallet");
            entity.Property(e => e.DefaultCurrency)
                .HasMaxLength(3)
                .HasColumnName("default_currency");
            entity.Property(e => e.Email).HasColumnName("email");

            entity.HasOne(d => d.WalletNavigation).WithOne(p => p.UserAdditionalDatum)
                .HasForeignKey<UserAdditionalDatum>(d => d.Wallet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_wallet_address");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.Address).HasName("wallet_pkey");

            entity.ToTable("wallet", "dbo");

            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CreationTimestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_timestamp");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Language)
                .HasMaxLength(2)
                .HasColumnName("language");
            entity.Property(e => e.DeleteDeadline)
                .HasColumnName("delete_deadline");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
        });
        
        modelBuilder.AddOutboxPattern();
    }
}