using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trivo.Domain.Models;

namespace Trivo.Infrastructure.Persistence.Configurations;

public class SanctionConfig : IEntityTypeConfiguration<Sanction>
{
    public void Configure(EntityTypeBuilder<Sanction> builder)
    {
        // Table Mapping
        builder.ToTable("Sanction");

        // Primary Key
        builder.HasKey(s => s.Id)
            .HasName("PKSanctionId");

        // Properties
        builder.Property(s => s.Id)
            .HasColumnName("PKSanctionId")
            .IsRequired();

        builder.Property(s => s.Type)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(s => s.Reason)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.Property(s => s.ExpiresAt)
            .IsRequired(false);

        builder.Property(s => s.RevokedAt)
            .IsRequired(false);

        builder.Property(s => s.UpdatedAt)
            .IsRequired(false);

        // Foreign Key Column Names
        builder.Property(s => s.UserId)
            .HasColumnName("FKUserId")
            .IsRequired();

        builder.Property(s => s.ReportId)
            .HasColumnName("FKReportId")
            .IsRequired(false);

        builder.Property(s => s.AdminId)
            .HasColumnName("FKAdminId")
            .IsRequired(false);

        // Relationships — Restrict everywhere: a sanction is part of the audit history and
        // must outlive any attempt to delete the rows it points to.
        builder.HasOne(s => s.User)
            .WithMany(u => u.Sanctions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Report)
            .WithOne(r => r.Sanction)
            .HasForeignKey<Sanction>(s => s.ReportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Admin)
            .WithMany()
            .HasForeignKey(s => s.AdminId)
            .OnDelete(DeleteBehavior.Restrict);

        // Login looks up "does this user have an active sanction" on every attempt.
        builder.HasIndex(s => new { s.UserId, s.ExpiresAt })
            .HasDatabaseName("IXSanctionUserExpiresAt");
    }
}
