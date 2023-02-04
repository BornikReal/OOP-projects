namespace Domain.Common.Exceptions;

public class WorkerActivityException : DomainException
{
    private WorkerActivityException(string? message)
        : base(message) { }

    public static WorkerActivityException MessageLogAlreadyInAccount(Guid sourceId)
        => new WorkerActivityException($"Message log from source {sourceId} is alreday in worker activity");
}