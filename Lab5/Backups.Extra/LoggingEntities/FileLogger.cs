namespace Backups.Extra.LoggingEntities;

public class FileLogger : ILogger
{
    private readonly bool _isPrefix;
    private readonly string _logFilePath;
    public FileLogger(string logFilePath, bool isPrefix)
    {
        _logFilePath = logFilePath;
        _isPrefix = isPrefix;
    }

    public void Log(string message)
    {
        message = $"\n{DateTime.Now:HH:mm:ss.fff}|{message}";
        if (_isPrefix)
            message = message.Replace("\n", $"\n{DateTime.Now:HH:mm:ss.fff}|");
        using StreamWriter writer = File.AppendText(_logFilePath);
        writer.WriteLine(message);
    }
}
