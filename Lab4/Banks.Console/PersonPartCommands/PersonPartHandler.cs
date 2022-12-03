using Banks.PersonBuilder;

namespace Banks.Console.BankPartCommands;

public abstract class PersonPartHandler
{
    private PersonPartHandler? _nextHandler;

    public PersonPartHandler SetNext(PersonPartHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual void HandleRequest(string? command, IPersonBuilder builder)
    {
        _nextHandler?.HandleRequest(command, builder);
    }
}
