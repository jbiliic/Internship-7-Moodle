using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moodle.Domain.Entities;

namespace Moodle.Infrastructure.Database.Configurations
{
    internal sealed class ConversationConfiguration
        : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.ToTable("conversations");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.User1Id)
                .HasColumnName("user1_id")
                .IsRequired();

            builder.Property(c => c.User2Id)
                .HasColumnName("user2_id")
                .IsRequired();

            builder.HasIndex(c => new { c.User1Id, c.User2Id })
                .IsUnique();

            builder.HasOne(c => c.User1)
                .WithMany()
                .HasForeignKey(c => c.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.User2)
                .WithMany()
                .HasForeignKey(c => c.User2Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
