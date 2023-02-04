namespace Application.Exceptions;

public class NotEnoughPermissionsException : ApplicationException
{
    private NotEnoughPermissionsException(string? message)
        : base(message) { }

    public static NotEnoughPermissionsException WorkerNotEnoughPermissionsException(string name)
        => new NotEnoughPermissionsException($"Worker {name} can't do this action");
}