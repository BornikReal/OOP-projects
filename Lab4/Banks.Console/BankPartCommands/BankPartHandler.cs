using Banks.BankBuilders;

namespace Banks.Console.BankPartCommands;

public abstract class BankPartHandler
{
    private BankPartHandler? _nextHandler;

    public BankPartHandler SetNext(BankPartHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual void HandleRequest(string? command, IBankBuilder bankBuilder)
    {
        _nextHandler?.HandleRequest(command, bankBuilder);
    }
}
