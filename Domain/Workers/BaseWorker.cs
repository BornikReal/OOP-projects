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

    public string Name { get; }
    public Guid Id { get; }
    public virtual AccessLayer Access { get; }
    public abstract IReadOnlyCollection<MessageLog> GetMessageLogs(DateTime time, TimeSpan duration);
}
