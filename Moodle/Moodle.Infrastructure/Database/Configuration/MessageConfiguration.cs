using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moodle.Domain.Common.Entities;
using Moodle.Domain.Entities;

namespace Moodle.Infrastructure.Database.Configurations
{
    internal sealed class MessageConfiguration
        : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("messages");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasColumnName("id");

            builder.Property(m => m.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(BaseEntity.MaxNameLen);

            builder.Property(m => m.Content)
                .HasColumnName("content")
                .IsRequired()
                .HasMaxLength(BaseEntity.MessageMaxLen);

            builder.Property(m => m.ConversationId)
                .HasColumnName("conversation_id")
                .IsRequired();

            builder.Property(m => m.SenderId)
                .HasColumnName("sender_id")
                .IsRequired();

            builder.HasOne(m => m.Conversation)
                .WithMany()
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
