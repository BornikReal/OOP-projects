using Banks.Console.Commands;
using Banks.Models;

namespace Banks.Console.AccountCreationManager;

public class AccountBalanceHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "4")
        {
            Guid id;
            while (true)
            {
                System.Console.WriteLine("Enter the id of account:");
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

            decimal result = CentralBank.GetMoney(id);
            System.Console.WriteLine($"Account balance: {result}");
        }

        base.HandleRequest(command);
    }
}