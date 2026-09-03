using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trivo.Domain.Models;

namespace Trivo.Infrastructure.Persistence.Configurations;

public class MessageConfig : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        // Table Mapping
        builder.ToTable("Message");

        // Primary Key
        builder.HasKey(m => m.MessageId)
            .HasName("PKMessageId");

        // Properties
        builder.Property(m => m.ChatId)
            .HasColumnName("FKChatId")
            .IsRequired();

        builder.Property(m => m.Content)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(m => m.SentAt)
            .IsRequired();

        builder.Property(m => m.Status)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(m => m.Type)
            .HasColumnType("varchar(50)");

        builder.Property(m => m.CreatedAt)
            .IsRequired();

        // Foreign Keys (from previous context establishmet)
        builder.Property(m => m.SenderId)
            .HasColumnName("FKSenderId")
            .IsRequired();

        builder.Property(m => m.ReceiverId)
            .HasColumnName("FKReceiverId")
            .IsRequired();

        // Relationships
        // Two FKs to User (Sender/Receiver) can't be inferred by convention, and both use
        // Restrict to avoid the multiple-cascade-paths error a delete on User would otherwise hit.
        builder.HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}