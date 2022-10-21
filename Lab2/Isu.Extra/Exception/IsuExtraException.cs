namespace Isu.Extra.Exception;

public abstract class IsuExtraException : IOException
{
    public IsuExtraException(string message)
        : base(message)
    { }
}
