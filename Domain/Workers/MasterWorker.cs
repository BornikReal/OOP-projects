using Domain.Accounts;
using Domain.Activity;

namespace Domain.Workers;

public class MasterWorker : BaseWorker
{
    private readonly HashSet<BaseWorker> _slaves;
    public MasterWorker(string name, Guid id, AccessLayer access) : base(name, id, access)
    {
        _slaves = new HashSet<BaseWorker>();
    }

    public void AddWorker(BaseWorker worker)
    {
        if (_slaves.Add(worker) is false)
            throw new ArgumentException("Worker already exists", nameof(worker));
    }

    public override IReadOnlyCollection<MessageLog> GetMessageLogs(DateTime time, TimeSpan duration)
    {
        return _slaves
            .SelectMany(x => x.GetMessageLogs(time, duration))
            .ToList();
    }

    public Report CreateReport(DateTime time, TimeSpan duration)
    {
        return new Report(GetMessageLogs(time, duration));
    }
}
