using Domain.Accounts;
using Domain.Activity;
using Domain.Messages;

namespace Domain.Workers;

public class SlaveWorker : BaseWorker
{
    public SlaveWorker(string name, Guid id, AccessLayer access)
        : base(name, id, access)
    { }

    protected SlaveWorker() { }
    protected virtual WorkerActivity Activity { get; set; } = new WorkerActivity();

    public void HandleMessage(BaseMessage message, Guid logId, Guid sourceId, DateTime time)
    {
        message.HandleMessage();
        Activity.AddMessageLog(new MessageLog(logId, sourceId, time));
    }

    public override IReadOnlyCollection<MessageLog> GetMessageLogs(DateTime time, TimeSpan duration)
    {
        return Activity.MessageLogs
            .Where(x => time - x.stateChangeTime <= duration)
            .ToList();
    }
}
