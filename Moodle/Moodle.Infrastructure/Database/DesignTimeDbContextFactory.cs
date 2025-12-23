using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Moodle.Infrastructure.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MoodleDbContext>
    {
        public MoodleDbContext CreateDbContext(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Moodle.Presentation"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("Database");
            var optionsBuilder = new DbContextOptionsBuilder<MoodleDbContext>();

            optionsBuilder.UseNpgsql(connectionString);

            return new MoodleDbContext(optionsBuilder.Options);
        }
    }
}