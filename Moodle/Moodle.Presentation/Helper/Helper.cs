using System.Globalization;
using System.Security.Cryptography;
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
        public static void displayValidationErrors(IReadOnlyList<Moodle.Application.Common.Model.ValidationResaultItem> errors)
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
                if(int.TryParse(toReturn, out var result))
                {
                    return result;
                }
                else
                {
                    Helper.clearDisplAndDisplMessage($"{inputType} must be a valid integer.");
                }
            }
        }
        public static void renderMessages(IReadOnlyList<MessageDTO> messages, int otherUserId) {
            foreach (var message in messages)
            {
                if (message.SenderId == otherUserId)
                {
                    Console.WriteLine($"Title: {message.Title} \n Content: {message.Content} \nSent at: {message.SentAt}");
                }
                else
                {
                    Console.WriteLine($"\t\t\t\t\tTitle: {message.Title} \n\t\t\t\t\tContent: {message.Content} \n\t\t\t\t\tSent at: {message.SentAt}");
                }
            }
        }
    }
}
