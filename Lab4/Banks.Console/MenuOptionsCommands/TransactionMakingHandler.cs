using Banks.Console.AccountCreationManager;
using Banks.Console.Commands;

namespace Banks.Console.MenuOptionsCommands;

public class TransactionMakingHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "4")
        {
            System.Console.WriteLine("Transactions");
            System.Console.WriteLine("Options:");
            System.Console.WriteLine("1. Deposit");
            System.Console.WriteLine("2. Withdraw");
            System.Console.WriteLine("3. Transfer");
            System.Console.WriteLine("4. Cancel transaction");
            var depositTransactionHandler = new DepositTransactionHandler();
            var withdrawTransactionHandler = new WithdrawTransactionHandler();
            var transferTransactionHandler = new TransferTransactionHandler();
            var cancelTransactionHandler = new CancelTransactionHandler();
            depositTransactionHandler.SetNext(withdrawTransactionHandler);
            withdrawTransactionHandler.SetNext(transferTransactionHandler);
            transferTransactionHandler.SetNext(cancelTransactionHandler);
            System.Console.Write("Your choice: ");
            depositTransactionHandler.HandleRequest(System.Console.ReadLine() !);
        }

        base.HandleRequest(command);
    }
}