using System.Text;

namespace SmartBook.Utilities;
public sealed class Log
{
    /// <summary>
    /// The path to the file where the log is stored.
    /// </summary>
    private static string FilePath => @"..\..\..\..\Smartbook\Data\";

    /// <summary>
    /// The name of the file where the log is stored.
    /// </summary>
    private static string FileName => "Log";
    /// <summary>
    /// The file extension of the file where the log is stored.
    /// </summary>
    private static string FileExtension => ".txt";

    /// <summary>
    /// The full path to the file where the log is stored.
    /// </summary>
    public static string PathToFile {
        get;
        private set;
    }

    /// <summary>
    /// Instance of the log class. Using Lazy<T> to ensure that the instance is created only when it is needed.
    /// </summary>
    private static readonly Lazy<Log> instance = new Lazy<Log>(static () => new Log());

    /// <summary>
    /// The singleton instance of the Log class. Using Lazy<T> to ensure that the instance is created only when it is needed.
    /// </summary>
    public static Log Instance => instance.Value;

    private Log() {
        PathToFile = Path.Combine(FilePath, FileName + FileExtension);
    }

    /// <summary>
    /// Logs a message to the log file.
    /// </summary>
    /// <param name="message"></param>
    public void LogMessage(string message) {
        ArgumentNullException.ThrowIfNull(message);
        //Open the file in append mode creates the file if it does not already exist.
        using StreamWriter writer = new StreamWriter(PathToFile, true);
        writer.WriteLine($"{DateTime.Now} - INFORMATION: {message}");
    }

    /// <summary>
    /// Logs an exception to the log file.
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="message">This message is optional.</param>
    public void LogMessage(Exception ex, string? message = null) {
        StringBuilder logMessage = new StringBuilder();

        logMessage.AppendLine($"{DateTime.Now} - ERROR: {ex.Message}");
        if(ex.InnerException != null) {
            logMessage.AppendLine($" - INNER EXCEPTION: {ex.InnerException.Message}");
        }
        if(message != null) {
            logMessage.AppendLine($" - INFORMATION: {message}");
        }
        //Open the file in append mode creates the file if it does not already exist.
        using StreamWriter writer = new StreamWriter(PathToFile, true);
        writer.WriteLine(logMessage.ToString());
    }


}
