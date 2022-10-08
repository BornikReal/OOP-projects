namespace Isu.Exception;

public abstract class IsuException : IOException
{
    public IsuException(string message)
        : base(message)
    { }
}