using Microsoft.EntityFrameworkCore;
using Moodle.Application.Common;
using Moodle.Domain.Entities;
using Moodle.Domain.Entities.Course;

namespace Moodle.Infrastructure.Database
{
    public sealed class MoodleDbContext : DbContext , IMoodleDbContext
    {
        public MoodleDbContext(DbContextOptions<MoodleDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Conversation> Conversations => Set<Conversation>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<IsEnrolled> Enrollments => Set<IsEnrolled>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseNotification> CourseNotifications => Set<CourseNotification>();
        public DbSet<LearningMaterials> LearningMaterials => Set<LearningMaterials>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(MoodleDbContext).Assembly);
            
        }
    }
}
