﻿using Domain.Accounts;
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
    public IReadOnlyCollection<BaseWorker> Slaves
    {
        get => SlavesList;
        protected set => SlavesList = value.ToList();
    }

    protected List<BaseWorker> SlavesList { get; set; }

    public void AddWorker(BaseWorker worker)
    {
        if (SlavesList.Contains(worker))
            throw new ArgumentException("Worker already exists", nameof(worker));
        SlavesList.Add(worker);
    }

    public override IReadOnlyCollection<MessageLog> GetMessageLogs(DateTime time, TimeSpan duration)
    {
        return SlavesList
            .SelectMany(x => x.GetMessageLogs(time, duration))
            .ToList();
    }

    public Report CreateReport(Guid id, DateTime time, TimeSpan duration)
    {
        return new Report(id, GetMessageLogs(time, duration));
    }
}
