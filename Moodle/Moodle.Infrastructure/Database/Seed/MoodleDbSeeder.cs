using Moodle.Domain.Entities;
using Moodle.Domain.Entities.Course;
using Moodle.Infrastructure.Database;

namespace Moodle.Infrastructure.Database;

public static class MoodleDbSeeder
{
    public static async Task SeedAsync(MoodleDbContext context)
    {
        // Prevent double seeding
        if (context.Users.Any())
            return;

        // ============================
        // USERS
        // ============================
        var users = new List<User>
        {
            // Students
            new() { FirstName = "Alice", LastName = "Student", Email = "alice@student.com", Password = "pw1" },
            new() { FirstName = "Bob", LastName = "Student", Email = "bob@student.com", Password = "pw2" },
            new() { FirstName = "Charlie", LastName = "Student", Email = "charlie@student.com", Password = "pw3" },
            new() { FirstName = "Diana", LastName = "Student", Email = "diana@student.com", Password = "pw4" },
            new() { FirstName = "Eve", LastName = "Student", Email = "eve@student.com", Password = "pw5" },
            new() { FirstName = "Frank", LastName = "Student", Email = "frank@student.com", Password = "pw6" },

            // Professors
            new() { FirstName = "Grace", LastName = "Hopper", Email = "grace@prof.com", Password = "pw7", IsProfessor = true },
            new() { FirstName = "Alan", LastName = "Turing", Email = "alan@prof.com", Password = "pw8", IsProfessor = true },
            new() { FirstName = "Ada", LastName = "Lovelace", Email = "ada@prof.com", Password = "pw9", IsProfessor = true },

            // Admin
            new() { FirstName = "Admin", LastName = "User", Email = "admin@system.com", Password = "adminpw", IsAdministrator = true }
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();

        // ============================
        // COURSES
        // ============================
        var courses = new List<Course>
        {
            new() { Name = "Databases", Major = "Computer Science", Semester = 3, ECTS = 6, ProfessorId = users[6].Id },
            new() { Name = "Operating Systems", Major = "Computer Science", Semester = 4, ECTS = 6, ProfessorId = users[6].Id },
            new() { Name = "Algorithms", Major = "Computer Science", Semester = 2, ECTS = 6, ProfessorId = users[7].Id },
            new() { Name = "Discrete Math", Major = "Computer Science", Semester = 1, ECTS = 5, ProfessorId = users[7].Id },
            new() { Name = "Software Engineering", Major = "Computer Science", Semester = 5, ECTS = 6, ProfessorId = users[6].Id }
        };

        context.Courses.AddRange(courses);
        await context.SaveChangesAsync();

        // ============================
        // ENROLLMENTS
        // ============================
        var enrollments = new List<IsEnrolled>
        {
            new() { UserId = users[0].Id, CourseId = courses[0].Id },
            new() { UserId = users[0].Id, CourseId = courses[1].Id },
            new() { UserId = users[1].Id, CourseId = courses[0].Id },
            new() { UserId = users[2].Id, CourseId = courses[2].Id },
            new() { UserId = users[3].Id, CourseId = courses[3].Id },
            new() { UserId = users[4].Id, CourseId = courses[4].Id }
        };

        context.Enrollments.AddRange(enrollments);
        await context.SaveChangesAsync();

        // ============================
        // COURSE NOTIFICATIONS
        // ============================
        var notifications = new List<CourseNotification>
        {
            new()
            {
                CourseId = courses[0].Id,
                ProfessorId = users[6].Id,
                Title = "Welcome",
                Content = "Welcome to the Databases course!"
            },
            new()
            {
                CourseId = courses[0].Id,
                ProfessorId = users[6].Id,
                Title = "Exam Info",
                Content = "Midterm exam will be held in week 8."
            },
            new()
            {
                CourseId = courses[2].Id,
                ProfessorId = users[7].Id,
                Title = "Homework 1",
                Content = "First homework has been released."
            }
        };

        context.CourseNotifications.AddRange(notifications);
        await context.SaveChangesAsync();

        // ============================
        // LEARNING MATERIALS
        // ============================
        var materials = new List<LearningMaterials>
        {
            new()
            {
                CourseId = courses[0].Id,
                UploaderId = users[6].Id,
                Title = "ER Diagrams",
                FilePath = "/files/db/er_diagrams.pdf"
            },
            new()
            {
                CourseId = courses[0].Id,
                UploaderId = users[6].Id,
                Title = "SQL Basics",
                FilePath = "/files/db/sql_basics.pdf"
            },
            new()
            {
                CourseId = courses[2].Id,
                UploaderId = users[7].Id,
                Title = "Sorting Algorithms",
                FilePath = "/files/algo/sorting.pdf"
            }
        };

        context.LearningMaterials.AddRange(materials);
        await context.SaveChangesAsync();

        // ============================
        // CONVERSATIONS
        // ============================
        var conversations = new List<Conversation>
        {
            new() { User1Id = users[0].Id, User2Id = users[6].Id },
            new() { User1Id = users[1].Id, User2Id = users[6].Id },
            new() { User1Id = users[2].Id, User2Id = users[7].Id }
        };

        context.Conversations.AddRange(conversations);
        await context.SaveChangesAsync();

        // ============================
        // MESSAGES
        // ============================
        var messages = new List<Message>
        {
            new() { ConversationId = conversations[0].Id, SenderId = users[0].Id, Title = "Question", Content = "I have a question about the exam." },
            new() { ConversationId = conversations[0].Id, SenderId = users[6].Id, Title = "Reply", Content = "Sure, let me know." },
            new() { ConversationId = conversations[1].Id, SenderId = users[1].Id, Title = "Help", Content = "Can you explain topic 3?" },
            new() { ConversationId = conversations[2].Id, SenderId = users[7].Id, Title = "Office Hours", Content = "Office hours are tomorrow at 10." }
        };

        context.Messages.AddRange(messages);
        await context.SaveChangesAsync();
    }
}
