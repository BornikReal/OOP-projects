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

    public string Name { get; }
    public Guid Id { get; }
    public AccessLayer Access { get; }
    public abstract IReadOnlyCollection<MessageLog> GetMessageLogs(DateTime time, TimeSpan duration);
}
