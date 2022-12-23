namespace Domain.Activity;

public class WorkerActivity
{
    private List<MessageLog> _messageLogs = new List<MessageLog>();
    public WorkerActivity() { }

    public virtual IReadOnlyCollection<MessageLog> MessageLogs
    {
        get => _messageLogs;
        protected init => _messageLogs = value.ToList();
    }

    public void AddMessageLog(MessageLog messageLog)
    {
        if (_messageLogs.Contains(messageLog))
            throw new InvalidOperationException("Message log already exists");
        _messageLogs.Add(messageLog);
    }
}
