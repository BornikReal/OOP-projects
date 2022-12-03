using Banks.Console.BankManagmentCommands;
using Banks.Console.Commands;

namespace Banks.Console.MenuOptionsCommands;

public class ClientManagmentHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "2")
        {
            System.Console.WriteLine("Bank management menu");
            System.Console.WriteLine("Options:");
            System.Console.WriteLine("1. Create new person");
            System.Console.WriteLine("2. Configure an existing person");
            var existingBankManagment = new ExistingBankHandler();
            var newBankManagment = new NewBankHandler();
            newBankManagment.SetNext(existingBankManagment);
            System.Console.Write("Your choice: ");
            newBankManagment.HandleRequest(System.Console.ReadLine() !);
            System.Console.Clear();
        }

        base.HandleRequest(command);
    }
}
