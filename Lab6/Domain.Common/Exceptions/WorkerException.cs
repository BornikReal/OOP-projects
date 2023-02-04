namespace Domain.Common.Exceptions;

public class WorkerException : DomainException
{
    private WorkerException(string? message)
        : base(message) { }

    public static WorkerException WorkerAlreadyServesMaster(string name)
        => new WorkerException($"Slave {name} is already served his master");
}