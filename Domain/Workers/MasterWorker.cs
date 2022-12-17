using Domain.Accounts;
using Domain.Activity;

namespace Domain.Workers;

public class MasterWorker : BaseWorker
{
    public MasterWorker(string name, Guid id, AccessLayer access)
        : base(name, id, access)
    {
        SlavesList = new List<BaseWorker>();
    }

#pragma warning disable CS8618
    protected MasterWorker() { }
    public IReadOnlyCollection<BaseWorker> Slaves => SlavesList;
    protected virtual List<BaseWorker> SlavesList { get; set; }

    public void AddWorker(BaseWorker worker)
    {
        if (!SlavesList.Contains(worker))
            throw new ArgumentException("Worker already exists", nameof(worker));
        SlavesList.Add(worker);
    }

    public override IReadOnlyCollection<MessageLog> GetMessageLogs(DateTime time, TimeSpan duration)
    {
        return SlavesList
            .SelectMany(x => x.GetMessageLogs(time, duration))
            .ToList();
    }

    public Report CreateReport(DateTime time, TimeSpan duration)
    {
        return new Report(GetMessageLogs(time, duration));
    }
}
