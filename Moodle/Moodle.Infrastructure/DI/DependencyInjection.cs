using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moodle.Application.Common;
using Moodle.Domain.Persistence.Repository;
using Moodle.Domain.Persistence.Repository.Common;
using Moodle.Infrastructure.Database;
using Moodle.Infrastructure.Repository;
using Moodle.Infrastructure.Repository.Common;

namespace Moodle.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<MoodleDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Database")));

        services.AddScoped<IMoodleDbContext>(sp =>
            sp.GetRequiredService<MoodleDbContext>());

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();

        return services;
    }
}
