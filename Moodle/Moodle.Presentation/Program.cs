using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moodle.Application.Common;
using Moodle.Application.Handlers.Auth;
using Moodle.Domain.Persistence.Repository;
using Moodle.Domain.Services.Validation;
using Moodle.Infrastructure.Database;
using Moodle.Infrastructure.Repository;
using Moodle.Presentation.Menus;
namespace Moodle.Presentation;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString =
            configuration.GetConnectionString("Database");

        // DbContext
        services.AddDbContext<MoodleDbContext>(options =>
            options.UseNpgsql(connectionString));

        // DbContext abstraction
        services.AddScoped<IMoodleDbContext>(sp =>
            sp.GetRequiredService<MoodleDbContext>());

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();

        // Domain / Application services
        services.AddScoped<UserValidationService>();
        services.AddScoped<CourseValidationService>();

        // Handlers
        services.AddScoped<RegistrationReqHandler>();
        services.AddScoped<LoginReqHandler>();


        services.AddScoped<LoginMenu>();

        // Build provider
        using var provider = services.BuildServiceProvider();

        // Create a scope (VERY IMPORTANT)
        using var scope = provider.CreateScope();

        // Resolve your entry handler
        var handler = scope.ServiceProvider
            .GetRequiredService<LoginMenu>();

        await handler.Handle();



    }
}
