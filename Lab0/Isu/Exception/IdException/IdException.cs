namespace Isu.Exception.IdException;

public abstract class IdException : IsuException
{
    public IdException(string message)
        : base(message)
    { }
}