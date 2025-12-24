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
    }
}
