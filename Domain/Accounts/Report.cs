using Domain.Activity;

namespace Domain.Accounts;

public class Report
{
    private List<MessageLog> _messageLogs;
    public Report(Guid id, IReadOnlyCollection<MessageLog> messageLogs)
    {
        _messageLogs = new List<MessageLog>(messageLogs);
        Id = id;
    }

#pragma warning disable CS8618
    protected Report() { }

    public Guid Id { get; protected set; }

    public virtual IReadOnlyCollection<MessageLog> MessageLogs
    {
        get => _messageLogs;
        protected init => _messageLogs = value.ToList();
    }
}
