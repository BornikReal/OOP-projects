namespace Banks.Console.Commands;

public abstract class BaseHandler
{
    private BaseHandler? _nextHandler;

    public BaseHandler SetNext(BaseHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual void HandleRequest(string command)
    {
        _nextHandler?.HandleRequest(command);
    }
}
