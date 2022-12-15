using Domain.Activity;

namespace Domain.Accounts;

public class Report
{
    private readonly HashSet<MessageLog> _messageLogs;

    public Report(IReadOnlyCollection<MessageLog> messageLogs)
    {
        _messageLogs = new HashSet<MessageLog>(messageLogs);
    }

    public int GetNumberOfMessagesProcessedBySubordinates() => _messageLogs.Count;

    public int GetNumberOfMessagesReceivedToASpecificSource(Guid sourceId) => _messageLogs.Count(x => x.sourceId == sourceId);

    public int GetNumberOfMessagesDuringTheRequestedInterval(DateTime timeStart, TimeSpan timeSpan) => _messageLogs.Count(x => x.stateChangeTime - timeStart < timeSpan);
}
