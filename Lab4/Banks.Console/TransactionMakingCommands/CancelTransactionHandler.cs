using Banks.Console.Commands;
using Banks.Models;
using Banks.Transactions;

namespace Banks.Console.AccountCreationManager;

public class CancelTransactionHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "4")
        {
            Guid id;
            while (true)
            {
                System.Console.WriteLine("Enter the id of the transaction:");
                string? input = System.Console.ReadLine();
                try
                {
                    id = Guid.Parse(input!);
                    break;
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine($"{input} is not a number!");
                }
            }

            CentralBank.CancelTransaction(id);
        }

        base.HandleRequest(command);
    }
}
