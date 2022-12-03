using Banks.AccountBuilders;
using Banks.Console.Commands;
using Banks.Models;

namespace Banks.Console.AccountCreationManager;

public class DebitAccountHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "1")
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

            Guid id = CentralBank.GetBank(idBank).CreateBankAccount(new DebitAccountFactory(CentralBank.GetPerson(idPerson)));
            System.Console.WriteLine($"Created a debit account with {id}");
        }

        base.HandleRequest(command);
    }
}
