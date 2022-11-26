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
        if (_isPrefix)
            message = $"{DateTime.Now:HH:mm:ss.fff}|{message}";
        using StreamWriter writer = File.AppendText(_logFilePath);
        writer.WriteLine(message);
    }
}
