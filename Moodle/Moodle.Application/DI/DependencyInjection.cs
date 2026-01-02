using Microsoft.Extensions.DependencyInjection;
using Moodle.Application.Handlers.Admin;
using Moodle.Application.Handlers.Auth;
using Moodle.Application.Handlers.Convo;
using Moodle.Application.Handlers.Professor;
using Moodle.Application.Handlers.Stats;
using Moodle.Application.Handlers.StudentCourse;
using Moodle.Domain.Services.Validation;

namespace Moodle.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UserValidationService>();
        services.AddScoped<CourseValidationService>();

        services.AddScoped<RegistrationReqHandler>();
        services.AddScoped<LoginReqHandler>();
        services.AddScoped<GetUserChatsHandler>();
        services.AddScoped<SendMessageHandler>();
        services.AddScoped<GetChatAndMessagesHandler>();

        services.AddScoped<GetEnrolledInHandler>();
        services.AddScoped<GetCourseNotifAndMatsHandler>();
        services.AddScoped<GetProfessorCoursesHandler>();
        services.AddScoped<GetUsersEnrolledInHandler>();
        services.AddScoped<GetAllStudentsHandler>();
        services.AddScoped<AddStudentHandler>();
        services.AddScoped<AddNotifAndMatsHandler>();

        services.AddScoped<DeleteUserHandler>();
        services.AddScoped<GetAllUsersHandler>();
        services.AddScoped<EditUserRoleHandler>();
        services.AddScoped<EditUserEmailHandler>();
        services.AddScoped<GetAllCoursesHandler>();
        services.AddScoped<GetAllProfHandler>();
        services.AddScoped<ReassignProfHandler>();

        services.AddScoped<GetCoursesPerEnrollHandler>();
        services.AddScoped<GetUsersPerNumMessage>();

        return services;
    }
}
