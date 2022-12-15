namespace Domain.Activity;

public class WorkerActivity
{
    private readonly HashSet<MessageLog> _messageLogs;

    public WorkerActivity()
    {
        _messageLogs = new HashSet<MessageLog>();
    }

    public IReadOnlyCollection<MessageLog> MessageLogs => _messageLogs;

    public void AddMessageLog(MessageLog messageLog)
    {
        if (_messageLogs.Add(messageLog) is false)
            throw new InvalidOperationException("Message log already exists");
    }
}
