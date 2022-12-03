using Banks.Console.Commands;
using Banks.Models;
using Banks.Transactions;

namespace Banks.Console.AccountCreationManager;

public class DepositTransactionHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "1")
        {
            Guid id;
            while (true)
            {
                System.Console.WriteLine("Enter the id of the account:");
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

            decimal result;
            while (true)
            {
                System.Console.WriteLine("Enter the amount:");
                string? input = System.Console.ReadLine();
                try
                {
                    result = Convert.ToDecimal(input!);
                    break;
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine($"{input} is not a number!");
                }
            }

            CentralBank.MakeTransaction(new DepositTransaction(id, result));
            System.Console.Clear();
        }

        base.HandleRequest(command);
    }
}
