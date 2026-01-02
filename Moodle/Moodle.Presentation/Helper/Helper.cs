using System.Globalization;
using System.Security.Cryptography;
using Moodle.Application.Common.Model;
using Moodle.Application.DTO;

namespace Moodle.Presentation.Helper
{
    internal static class Helper
    {
        public static void clearDisplAndDisplMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.ReadKey();
        }
        public static string getString(string inputType)
        {
            while (true)
            {
                Console.Write($"\nEnter {inputType}: ");
                var toReturn = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(toReturn))
                {
                    Console.Write($"\n{inputType} can't be whitespace");
                    Console.ReadKey();
                    continue;
                }
                Console.Clear();
                return toReturn;
            }
        }
        public static string? getStringOptional(string inputType)
        {
            while (true)
            {
                Console.Write($"\nEnter {inputType}: ");
                var toReturn = Console.ReadLine();
                return toReturn;
            }
        }
        public static DateTimeOffset? getDateOfBirth(string inputType)
        {
            Console.Write($"Enter {inputType} like dd-mm-yyyy: ");
            var input = Console.ReadLine();
            if (DateTimeOffset.TryParseExact(
            input,
            "dd-MM-yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var dateOfBirth))
            {
                return dateOfBirth;
            }
            return null;
        }
        public static void displayValidationErrors(IReadOnlyList<Moodle.Application.Common.Model.ValidationResultItem> errors)
        {
            Console.Clear();
            Console.WriteLine("The following errors occurred:");
            foreach (var error in errors)
            {
                Console.WriteLine($"Code : {error.Code}");
                Console.WriteLine($"Message : {error.Message} \n");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public static void timeOut30s()
        {
            for (int i = 30; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"You will be redirected in {i} seconds...");
                System.Threading.Thread.Sleep(1000);
            }
        }
        public static string generateCaptcha(int length = 6)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

            var bytes = new byte[length];
            RandomNumberGenerator.Fill(bytes);

            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[bytes[i] % chars.Length];
            }

            return new string(result);
        }
        public static int getAndValidateInputInt(string inputType)
        {
            while (true)
            {
                Console.Write($"Enter {inputType}: ");
                var toReturn = Console.ReadLine();
                if (int.TryParse(toReturn, out var result))
                {
                    return result;
                }
                else
                {
                    Helper.clearDisplAndDisplMessage($"{inputType} must be a valid integer.");
                }
            }
        }
        public static void renderMessages(IReadOnlyList<MessageDTO> messages, int otherUserId)
        {
            foreach (var message in messages)
            {
                if (message.SenderId == otherUserId)
                {
                    Console.WriteLine($"Title: {message.Title} \n Content: {message.Content} \nSent at: {message.SentAt}\n");
                }
                else
                {
                    Console.WriteLine($"\t\t\t\t\t\tTitle: {message.Title} \n\t\t\t\t\t\tContent: {message.Content} \n\t\t\t\t\t\tSent at: {message.SentAt}\n");
                }
            }
        }
        public static void renderCourseNotifs(GetAllResponse<CourseNotifDTO> notifs)
        {
            Console.Clear();
            if (notifs.isEmpty)
            {
                clearDisplAndDisplMessage("So empty...");
                return;
            }
            foreach (var notif in notifs.Items)
            {
                Console.WriteLine($"Title: {notif.Title} \n Content: {notif.Content} \nSent at: {notif.CreatedAt} by {notif.ProfessorEmail}");
            }
            Console.ReadKey();
        }
        public static void renderMats(GetAllResponse<MaterialsDTO> mats)
        {
            Console.Clear();
            if (mats.isEmpty)
            {
                clearDisplAndDisplMessage("So empty...");
                return;
            }
            foreach (var mat in mats.Items)
            {
                Console.WriteLine($"Title: {mat.Title} \n URL: {mat.FilePath} \nSent at: {mat.UploadedAt}");
            }
            Console.ReadKey();
        }
        public static void renderSortedUsers(GetAllResponse<UserDTO> users)
        {
            Console.Clear();
            if (users == null || users.isEmpty)
            {
                clearDisplAndDisplMessage("Where is everybody???");
                return;
            }
            var names = new List<UserDTO>();
            var noNames = new List<UserDTO>();
            foreach (var user in users.Items)
            {
                if (string.IsNullOrWhiteSpace(user.FirstName))
                {
                    noNames.Add(user);
                }
                else
                {
                    names.Add(user);
                }
            }
            names = names
                .OrderBy(u => u.FirstName)
                .ToList();
            noNames = noNames
                .OrderBy(u => u.Email)
                .ToList();
            Console.WriteLine("Named students:");
            foreach (var name in names)
            {
                Console.WriteLine($" ID : {name.Id} , Name : {name.FirstName} , Mail : {name.Email}");  
            }
            Console.WriteLine("\nAnonymous students:");
            foreach(var noName in noNames)
            {
                Console.WriteLine($" ID : {noName.Id} , Mail : {noName.Email}");
            }
            Console.ReadKey();
        }
        public static bool waitForConfirmation()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("\nAre you sure (y/n): ");
                switch (Console.ReadLine())
                {
                    case "Y":
                    case "y":
                    case "1":
                        Console.Clear();
                        return true;
                    case "N":
                    case "n":
                    case "0":
                        Console.Clear();
                        return false;
                    default:
                        Console.Write("\nInvalid input.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public static int displayUsers(List<UserDTO> items)
        {
            Console.Clear();
            var i = 0;
            foreach (var item in items)
            {
                Console.WriteLine($" {++i}. {item.FirstName} ({item.Email})");
            }
            return i;
        }
    }
}
