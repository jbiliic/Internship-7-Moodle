using Microsoft.Extensions.DependencyInjection;
using Moodle.Presentation.Menus.Admin;
using Moodle.Presentation.Menus.Common;
using Moodle.Presentation.Menus.Professor;
using Moodle.Presentation.Menus.Student;

namespace Moodle.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<LoginMenu>();
        services.AddScoped<MainMenu>();
        services.AddScoped<MenuRouter>();
        services.AddScoped<ChatMenu>();

        services.AddScoped<StudentCourseMenu>();
        services.AddScoped<ProfessorCourseMenu>();
        services.AddScoped<ManageCourseMenu>();
        services.AddScoped<ManageUsersMenu>();
        services.AddScoped<StatisticsMenu>();

        return services;
    }
}
