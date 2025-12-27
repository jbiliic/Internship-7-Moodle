using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moodle.Domain.Common.Entities;
using Moodle.Domain.Entities.Course;

namespace Moodle.Infrastructure.Database.Configurations.Course
{
    internal sealed class CourseNotificationConfiguration
        : IEntityTypeConfiguration<CourseNotification>
    {
        public void Configure(EntityTypeBuilder<CourseNotification> builder)
        {
            builder.ToTable("course_notifications");

            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id)
                .HasColumnName("id");

            builder.Property(n => n.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(BaseEntity.MaxNameLen);

            builder.Property(n => n.Content)
                .HasColumnName("content")
                .IsRequired()
                .HasMaxLength(BaseEntity.MessageMaxLen);

            builder.Property(n => n.CourseId)
                .HasColumnName("course_id")
                .IsRequired();

            builder.Property(n => n.ProfessorId)
                .HasColumnName("professor_id")
                .IsRequired();

            builder.HasOne(n => n.Course)
                .WithMany()
                .HasForeignKey(n => n.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(n => n.Professor)
                .WithMany()
                .HasForeignKey(n => n.ProfessorId)
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
