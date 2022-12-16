﻿using Domain.Accounts;
using Domain.Activity;
using Domain.Messages;

namespace Domain.Workers;

public class SlaveWorker : BaseWorker
{
    private readonly WorkerActivity _activity;
    public SlaveWorker(string name, Guid id, AccessLayer access)
        : base(name, id, access)
    {
        _activity = new WorkerActivity();
    }

    public void HandleMessage(BaseMessage message, Guid sourceId, DateTime time)
    {
        message.HandleMessage();
        _activity.AddMessageLog(new MessageLog(sourceId, time));
    }

    public override IReadOnlyCollection<MessageLog> GetMessageLogs(DateTime time, TimeSpan duration)
    {
        return _activity.MessageLogs
            .Where(x => time - x.stateChangeTime <= duration)
            .ToList();
    }
}
