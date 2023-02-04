using Domain.Accounts;
using Domain.Activity;

namespace Domain.Workers;

public abstract class BaseWorker
{
    public BaseWorker(string name, Guid id, AccessLayer access)
    {
        Name = name;
        Id = id;
        Access = access;
    }

#pragma warning disable CS8618
    protected BaseWorker() { }

    public string Name { get; protected init; }
    public Guid Id { get; protected init; }
    public virtual AccessLayer Access { get; protected init; }
    public abstract IReadOnlyCollection<MessageLog> GetMessageLogs(DateTime time, TimeSpan duration);
}
