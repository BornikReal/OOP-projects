using Banks.Console.BankPartCommands;
using Banks.PersonBuilder;

namespace Banks.Console.PersonPartCommands;

public class PersonPassportHandler : PersonPartHandler
{
    public override void HandleRequest(string? command, IPersonBuilder builder)
    {
        if (command is null or "4")
        {
            System.Console.WriteLine("Enter person passport or write 'skip' to skip this step:");
            string? input = System.Console.ReadLine();
            if (input != "skip")
                builder.SetPassportData(input!);
        }

        base.HandleRequest(command, builder);
    }
}
