using Banks.Console.MenuOptionsCommands;
using Banks.DateObservers;
using Banks.Models;

namespace Banks.Console;

public class Client
{
    public static void Main()
    {
        CentralBank.GetInstance(new DefaultClock());
        while (true)
        {
            System.Console.WriteLine("Welcome to the club, buddy!");
            System.Console.WriteLine("Options:");
            System.Console.WriteLine("1. Bank management");
            System.Console.WriteLine("2. Client management");
            System.Console.WriteLine("3. Open account");
            System.Console.WriteLine("4. Make transaction");
            System.Console.WriteLine("5. Made in heaven");
            System.Console.WriteLine("6. Exit");
            System.Console.Write("Your choice: ");
            string? choice = System.Console.ReadLine();
            var bankManagment = new BankManagmentHandler();
            var personManagment = new ClientManagmentHandler();
            var openAccount = new AccountOpeningHandler();
            var makeTransaction = new TransactionMakingHandler();
            var madeInHeavenHandler = new MadeInHeavenHandler();
            var exitHandler = new ExitHandler();
            bankManagment.SetNext(personManagment);
            personManagment.SetNext(openAccount);
            openAccount.SetNext(makeTransaction);
            makeTransaction.SetNext(madeInHeavenHandler);
            madeInHeavenHandler.SetNext(exitHandler);
            bankManagment.HandleRequest(choice!);
        }
    }
}
