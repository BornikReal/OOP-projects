using Banks.Console.BankPartCommands;
using Banks.PersonBuilder;

namespace Banks.Console.PersonPartCommands;

public class PersonSurnameHandler : PersonPartHandler
{
    public override void HandleRequest(string? command, IPersonBuilder builder)
    {
        if (command is null or "2")
        {
            System.Console.WriteLine("Enter person surname:");
            builder.SetSurname(System.Console.ReadLine() !);
        }

        base.HandleRequest(command, builder);
    }
}