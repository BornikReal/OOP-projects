using Banks.Console.BankPartCommands;
using Banks.PersonBuilder;

namespace Banks.Console.PersonPartCommands;

public class PersonAddressHandler : PersonPartHandler
{
    public override void HandleRequest(string? command, IPersonBuilder builder)
    {
        if (command is null or "3")
        {
            System.Console.WriteLine("Enter person address or write 'skip' to skip this step:");
            string? input = System.Console.ReadLine();
            if (input != "skip")
                builder.SetAdress(input!);
        }

        base.HandleRequest(command, builder);
    }
}
