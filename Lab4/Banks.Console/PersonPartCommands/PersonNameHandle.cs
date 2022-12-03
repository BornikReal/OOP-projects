using Banks.Console.BankPartCommands;
using Banks.PersonBuilder;

namespace Banks.Console.PersonPartCommands;

public class PersonNameHandle : PersonPartHandler
{
    public override void HandleRequest(string? command, IPersonBuilder builder)
    {
        if (command is null or "1")
        {
            System.Console.WriteLine("Enter person name:");
            builder.SetName(System.Console.ReadLine() !);
        }

        base.HandleRequest(command, builder);
    }
}
