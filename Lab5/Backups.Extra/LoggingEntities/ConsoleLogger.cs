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
        message = $"\n{DateTime.Now:HH:mm:ss.fff}|{message}";
        if (_isPrefix)
            message = message.Replace("\n", $"\n{DateTime.Now:HH:mm:ss.fff}|");
        Console.WriteLine(message);
    }
}
