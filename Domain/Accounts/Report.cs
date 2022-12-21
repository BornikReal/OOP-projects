using Domain.Activity;

namespace Domain.Accounts;

public class Report
{
    public Report(Guid id, IReadOnlyCollection<MessageLog> messageLogs)
    {
        MessageLogsList = new List<MessageLog>(messageLogs);
        Id = id;
    }

#pragma warning disable CS8618
    protected Report() { }

    public Guid Id { get; protected set; }

    public IReadOnlyCollection<MessageLog> MessageLogs => MessageLogsList;
    protected virtual List<MessageLog> MessageLogsList { get; set; }
}
