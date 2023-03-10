using Banks.Console.BankManagmentCommands;
using Banks.Console.Commands;

namespace Banks.Console.MenuOptionsCommands;

public class BankManagmentHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "1")
        {
            System.Console.WriteLine("Bank management menu");
            System.Console.WriteLine("Options:");
            System.Console.WriteLine("1. Create new bank");
            System.Console.WriteLine("2. Configure an existing bank");
            var existingBankManagment = new ExistingBankHandler();
            var newBankManagment = new NewBankHandler();
            newBankManagment.SetNext(existingBankManagment);
            System.Console.Write("Your choice: ");
            newBankManagment.HandleRequest(System.Console.ReadLine() !);
        }

        base.HandleRequest(command);
    }
}
