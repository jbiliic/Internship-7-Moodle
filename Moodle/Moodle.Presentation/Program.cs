using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moodle.Application;
using Moodle.Infrastructure;
using Moodle.Infrastructure.Database;
using Moodle.Presentation.Menus.Common;
namespace Moodle.Presentation;

internal class Program
{
    private static async Task Main(string[] args)
    {
        

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var services = new ServiceCollection();

        services
            .AddInfrastructure(configuration)
            .AddApplication()
            .AddPresentation();

        using var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<MoodleDbContext>();
        await db.Database.MigrateAsync();
        await MoodleDbSeeder.SeedAsync(db);

        var menu = scope.ServiceProvider.GetRequiredService<LoginMenu>();
        await menu.ShowAsync(null);
    }
}
