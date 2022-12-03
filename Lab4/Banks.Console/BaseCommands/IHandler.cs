namespace Banks.Console.BaseCommands;

public interface IHandler
{
    IHandler SetNext(IHandler handler);
    public abstract void HandleRequest(string command);
}
