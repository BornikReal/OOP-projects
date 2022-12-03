using Banks.Console.Commands;
using Banks.Models;
using Banks.Transactions;

namespace Banks.Console.AccountCreationManager;

public class TransferTransactionHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "3")
        {
            Guid id1;
            while (true)
            {
                System.Console.WriteLine("Enter the id of the sender account:");
                string? input = System.Console.ReadLine();
                try
                {
                    id1 = Guid.Parse(input!);
                    break;
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine($"{input} is not a number!");
                }
            }

            Guid id2;
            while (true)
            {
                System.Console.WriteLine("Enter the id of the receiver account:");
                string? input = System.Console.ReadLine();
                try
                {
                    id2 = Guid.Parse(input!);
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

            CentralBank.MakeTransaction(new TransferTransaction(id1, id2, result));
            System.Console.Clear();
        }

        base.HandleRequest(command);
    }
}
