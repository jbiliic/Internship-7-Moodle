using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moodle.Domain.Common.Entities;
using Moodle.Domain.Entities.Course;

namespace Moodle.Infrastructure.Database.Configurations.Course
{
    internal sealed class CourseConfiguration : IEntityTypeConfiguration<Domain.Entities.Course.Course>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Course.Course> builder)
        {
            builder.ToTable("courses");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(BaseEntity.MaxNameLen);

            builder.Property(c => c.Major)
                .HasColumnName("major")
                .IsRequired()
                .HasMaxLength(BaseEntity.MaxNameLen);

            builder.Property(c => c.Semester)
                .HasColumnName("semester")
                .IsRequired();

            builder.Property(c => c.ECTS)
                .HasColumnName("ects")
                .IsRequired();

            builder.Property(c => c.ProfessorId)
                .HasColumnName("professor_id")
                .IsRequired();

            builder.HasOne(c => c.Professor)
                .WithMany()
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
