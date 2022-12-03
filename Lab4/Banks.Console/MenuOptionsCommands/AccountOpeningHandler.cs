using Banks.Console.AccountCreationManager;
using Banks.Console.Commands;

namespace Banks.Console.MenuOptionsCommands;

public class AccountOpeningHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "3")
        {
            System.Console.WriteLine("Account opening menu");
            System.Console.WriteLine("Options:");
            System.Console.WriteLine("1. Create a debit account");
            System.Console.WriteLine("2. Create a deposit account");
            System.Console.WriteLine("3. Create a credit account");
            var debitAccountHandler = new DebitAccountHandler();
            var depositAccountHandler = new DepositAccountHandler();
            var creditAccountHandler = new CreditAccountHandler();
            debitAccountHandler.SetNext(depositAccountHandler);
            depositAccountHandler.SetNext(creditAccountHandler);
            System.Console.Write("Your choice: ");
            debitAccountHandler.HandleRequest(System.Console.ReadLine() !);
        }

        base.HandleRequest(command);
    }
}