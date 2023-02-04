namespace Domain.Common.Exceptions;

public class AccessLayerException : DomainException
{
    private AccessLayerException(string? message)
        : base(message) { }

    public static AccessLayerException InvalidAccessLayer(int access)
        => new AccessLayerException($"Access number {access} isn't not negative number");

    public static AccessLayerException InsufficientPermissions(int accessRequired, int accessActual)
        => new AccessLayerException($"Not enough permissions. Your access number {accessActual} is higher than reqired {accessRequired}");
}
