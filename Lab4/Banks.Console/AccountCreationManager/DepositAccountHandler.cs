using Banks.AccountBuilders;
using Banks.Console.Commands;
using Banks.Models;

namespace Banks.Console.AccountCreationManager;

public class DepositAccountHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "2")
        {
            Guid idBank, idPerson;
            while (true)
            {
                System.Console.WriteLine("Enter the id of the bank:");
                string? input = System.Console.ReadLine();
                try
                {
                    idBank = Guid.Parse(input!);
                    break;
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine($"{input} is not a number!");
                }
            }

            while (true)
            {
                System.Console.WriteLine("Enter the id of the person:");
                string? input = System.Console.ReadLine();
                try
                {
                    idPerson = Guid.Parse(input!);
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
                System.Console.WriteLine("Enter the start balance:");
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

            Guid id = CentralBank.GetBank(idBank).CreateBankAccount(new DepositAccountFactory(result, CentralBank.GetPerson(idPerson)));
            System.Console.WriteLine($"Created a deposit account with {id}");
        }

        base.HandleRequest(command);
    }
}