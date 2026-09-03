using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trivo.Domain.Models;

namespace Trivo.Infrastructure.Persistence.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table Mapping
        builder.ToTable("User");

        // Primary Key
        builder.HasKey(u => u.Id)
            .HasName("PKUserId");

        // Properties
        builder.Property(u => u.Id)
            .HasColumnName("PKUserId")
            .IsRequired();

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(u => u.Biography)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("UQUserEmail");

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Position)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(u => u.Username)
            .IsUnique()
            .HasDatabaseName("UQUserUsername");

        builder.Property(u => u.ProfilePicture)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(u => u.LinkedIn)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(u => u.Location)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(u => u.IsAccountConfirmed)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired(false);

        builder.Property(u => u.UserStatus)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(u => u.ProfileEmbedding)
            .HasColumnType("vector(1536)")
            .IsRequired(false);

        builder.Property(u => u.ProfileTextHash)
            .HasMaxLength(64)
            .IsRequired(false);
    }
}