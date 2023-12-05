
namespace BusinessLogicLayer.Helpers;

public class LoggingService : ILoggingService
{
    public void LogError(string message)
    {
        WriteToFile(message, "ERROR");
    }

    public void LogInfo(string message)
    {
        WriteToFile(message, "INFO");
    }

    public void LogWarning(string message)
    {
        WriteToFile(message, "WARNING");
    }

    private static void WriteToFile(string message, string type)
    {
        try
        {
            string directory = DateTime.Now.ToString("MMMM");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"logs\\{directory}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = Path.Combine(path, $"{DateTime.Now:yyyy-MM-dd}.txt");

            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.Write($"[{type}] : ");
                streamWriter.WriteLine(message);
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions related to file writing
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }
}