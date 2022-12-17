using Domain.Activity;

namespace Domain.Accounts;

public class Report
{
    public Report(IReadOnlyCollection<MessageLog> messageLogs)
    {
        MessageLogs = new List<MessageLog>(messageLogs);
    }

#pragma warning disable CS8618
    protected Report() { }
    protected virtual List<MessageLog> MessageLogs { get; set; }

    public int GetNumberOfMessagesProcessedBySubordinates() => MessageLogs.Count;

    public int GetNumberOfMessagesReceivedToASpecificSource(Guid sourceId) => MessageLogs.Count(x => x.sourceId == sourceId);

    public int GetNumberOfMessagesDuringTheRequestedInterval(DateTime timeStart, TimeSpan timeSpan) => MessageLogs.Count(x => x.stateChangeTime - timeStart < timeSpan);
}
