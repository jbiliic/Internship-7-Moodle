using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moodle.Domain.Entities;

namespace Moodle.Infrastructure.Database.Configurations
{
    internal sealed class IsEnrolledConfiguration
        : IEntityTypeConfiguration<IsEnrolled>
    {
        public void Configure(EntityTypeBuilder<IsEnrolled> builder)
        {
            builder.ToTable("enrollments");

            builder.HasKey(e => new { e.UserId, e.CourseId });

            builder.Property(e => e.UserId)
                .HasColumnName("user_id");

            builder.Property(e => e.CourseId)
                .HasColumnName("course_id");

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId)
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
