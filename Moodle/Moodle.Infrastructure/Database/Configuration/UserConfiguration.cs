using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moodle.Domain.Common.Entities;
using Moodle.Domain.Entities;

namespace Moodle.Infrastructure.Database.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(BaseEntity.MaxNameLen);

            builder.Property(u => u.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(BaseEntity.MaxNameLen);

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(u => u.DateOfBirth)
                .HasColumnName("date_of_birth");

            builder.Property(u => u.IsProfessor)
                .HasColumnName("is_professor")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.IsAdministrator)
                .HasColumnName("is_administrator")
                .IsRequired()
                .HasDefaultValue(false);

        }
    }
}
