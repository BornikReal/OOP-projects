namespace Backups.Extra.LoggingEntities;

public class ConsoleLogger : ILogger
{
    private readonly bool _isPrefix;
    public ConsoleLogger(bool isPrefix)
    {
        _isPrefix = isPrefix;
    }

    public void Log(string message)
    {
        if (_isPrefix)
            message = $"{DateTime.Now:HH:mm:ss.fff}|{message}";
        Console.WriteLine(message);
    }
}
