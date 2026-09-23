using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trivo.Domain.Models;

namespace Trivo.Infrastructure.Persistence.Configurations;

public class ReportConfig : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        // Table Mapping
        builder.ToTable("Report");

        // Primary Key
        builder.HasKey(r => r.ReportId)
            .HasName("PKReportId");

        // Properties
        builder.Property(r => r.ReportId)
            .HasColumnName("PKReportId")
            .IsRequired();

        builder.Property(r => r.Note)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(r => r.ReportStatus)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(r => r.ReportType)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(r => r.ReportedContent)
            .HasColumnType("text")
            .IsRequired(false);

        builder.Property(r => r.ReportedContentType)
            .HasColumnType("varchar(50)")
            .IsRequired(false);

        builder.Property(r => r.FinalReason)
            .HasColumnType("text")
            .IsRequired(false);

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.Property(r => r.ReviewedAt)
            .IsRequired(false);

        // Foreign Key Column Names
        builder.Property(r => r.ReportedById)
            .HasColumnName("FKReportedById")
            .IsRequired();

        builder.Property(r => r.ReportedUserId)
            .HasColumnName("FKReportedUserId")
            .IsRequired();

        builder.Property(r => r.MessageId)
            .HasColumnName("FKMessageId")
            .IsRequired(false);

        builder.Property(r => r.ReviewedByAdminId)
            .HasColumnName("FKReviewedByAdminId")
            .IsRequired(false);

        // Relationships
        // Two FKs to User (reporter / reported) can't be inferred by convention. Restrict keeps a
        // report (and its evidence) from being cascaded away if a user row is ever removed.
        builder.HasOne(r => r.Reporter)
            .WithMany(u => u.ReportsMade)
            .HasForeignKey(r => r.ReportedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ReportedUser)
            .WithMany(u => u.ReportsReceived)
            .HasForeignKey(r => r.ReportedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // The reported content is snapshotted on the report itself, so deleting the message
        // must not delete the report.
        builder.HasOne(r => r.Message)
            .WithMany(m => m.Reports)
            .HasForeignKey(r => r.MessageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(r => r.ReviewedByAdmin)
            .WithMany()
            .HasForeignKey(r => r.ReviewedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => r.ReportStatus)
            .HasDatabaseName("IXReportStatus");

        builder.HasIndex(r => r.CreatedAt)
            .HasDatabaseName("IXReportCreatedAt");
    }
}
