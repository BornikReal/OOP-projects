namespace Isu.Exception.InvalidGroupNameException;
public abstract class InvalidGroupNameException : IsuException
{
    public InvalidGroupNameException(string message)
        : base(message)
    { }
}