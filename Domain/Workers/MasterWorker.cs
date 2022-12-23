using Domain.Accounts;
using Domain.Activity;

namespace Domain.Workers;

public class MasterWorker : BaseWorker
{
    private List<BaseWorker> _slaves = new List<BaseWorker>();

    public MasterWorker(string name, Guid id, AccessLayer access)
        : base(name, id, access)
    { }

    protected MasterWorker() { }
    public virtual IReadOnlyCollection<BaseWorker> Slaves
    {
        get => _slaves;
        protected init => _slaves = value.ToList();
    }

    public void AddWorker(BaseWorker worker)
    {
        if (_slaves.Contains(worker))
            throw new ArgumentException("Worker already exists", nameof(worker));
        _slaves.Add(worker);
    }

    public override IReadOnlyCollection<MessageLog> GetMessageLogs(DateTime time, TimeSpan duration)
    {
        return _slaves
            .SelectMany(x => x.GetMessageLogs(time, duration))
            .ToList();
    }

    public Report CreateReport(Guid id, DateTime time, TimeSpan duration)
    {
        return new Report(id, GetMessageLogs(time, duration));
    }
}
