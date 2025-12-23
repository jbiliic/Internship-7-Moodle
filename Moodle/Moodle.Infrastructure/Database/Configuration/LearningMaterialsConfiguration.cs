using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moodle.Domain.Common.Entities;
using Moodle.Domain.Entities.Course;

namespace Moodle.Infrastructure.Database.Configurations.Course
{
    internal sealed class LearningMaterialsConfiguration
        : IEntityTypeConfiguration<LearningMaterials>
    {
        public void Configure(EntityTypeBuilder<LearningMaterials> builder)
        {
            builder.ToTable("learning_materials");

            builder.HasKey(lm => lm.Id);

            builder.Property(lm => lm.Id)
                .HasColumnName("id");

            builder.Property(lm => lm.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(BaseEntity.MessageMaxLen);

            builder.Property(lm => lm.FilePath)
                .HasColumnName("file_path")
                .IsRequired()
                .HasMaxLength(BaseEntity.MessageMaxLen);

            builder.Property(lm => lm.CourseId)
                .HasColumnName("course_id")
                .IsRequired();

            builder.Property(lm => lm.UploaderId)
                .HasColumnName("uploader_id")
                .IsRequired();

            builder.HasOne(lm => lm.Course)
                .WithMany()
                .HasForeignKey(lm => lm.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(lm => lm.Uploader)
                .WithMany()
                .HasForeignKey(lm => lm.UploaderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
