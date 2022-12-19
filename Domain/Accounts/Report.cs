using Domain.Activity;

namespace Domain.Accounts;

public class Report
{
    public Report(IReadOnlyCollection<MessageLog> messageLogs)
    {
        MessageLogsList = new List<MessageLog>(messageLogs);
    }

#pragma warning disable CS8618
    protected Report() { }
    public IReadOnlyCollection<MessageLog> MessageLogs => MessageLogsList;
    protected virtual List<MessageLog> MessageLogsList { get; set; }
}
