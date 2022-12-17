namespace Domain.Activity;

public class WorkerActivity
{
    public WorkerActivity() { }

    public IReadOnlyCollection<MessageLog> MessageLogs => MessageLogsList;
    protected virtual List<MessageLog> MessageLogsList { get; set; } = new List<MessageLog>();

    public void AddMessageLog(MessageLog messageLog)
    {
        if (!MessageLogsList.Contains(messageLog))
            throw new InvalidOperationException("Message log already exists");
        MessageLogsList.Add(messageLog);
    }
}
