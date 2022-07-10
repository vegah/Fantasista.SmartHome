using System.Globalization;

namespace Fantasista.SmartHome
{
    public static class Log
    {
        public static void Debug(string message)
        {            
            Console.WriteLine($"{DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)} - {message}");
        }

        public static void Info(string message)
        {            
            Console.WriteLine($"{DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)} - {message}");
        }

    }
}