using Banks.BankBuilders;
using Banks.Console.BankPartCommands;
using Banks.Console.Commands;
using Banks.Models;

namespace Banks.Console.BankManagmentCommands;

public class ExistingBankHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "2")
        {
            Guid result;
            while (true)
            {
                System.Console.WriteLine("Enter bank id:");
                string? input = System.Console.ReadLine();
                try
                {
                    result = Guid.Parse(input!);
                    break;
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine($"{input} is not a number!");
                }
            }

            Bank bank = CentralBank.GetBank(result);
            var bankBuilder = new DefaultBankBuilder(bank);
            System.Console.WriteLine("Choose option to edit");
            System.Console.WriteLine("Options:");
            System.Console.WriteLine("1. Credit comission");
            System.Console.WriteLine("2. Credit limit");
            System.Console.WriteLine("3. Debit interest rate");
            System.Console.WriteLine("4. Transfer limit");
            System.Console.WriteLine("5. Deposit account span");
            System.Console.WriteLine("6. Deposit interest rate strategy");
            System.Console.Write("Your choice: ");
            string? choice = System.Console.ReadLine();

            var creditComissionHandler = new CreditComissionHandler();
            var creditLimitHandler = new CreditLimitHandler();
            var debitInterestRateHandler = new DebitInterestRateHandler();
            var transferLimitHandler = new TransferLimitHandler();
            var depositAccountSpanHandler = new DepositAccountSpanHandler();
            var depositInterestRateStrategyHandler = new DepositInterestRateStrategyHandler();
            creditComissionHandler.SetNext(depositInterestRateStrategyHandler);
            depositInterestRateStrategyHandler.SetNext(depositAccountSpanHandler);
            depositAccountSpanHandler.SetNext(transferLimitHandler);
            transferLimitHandler.SetNext(creditLimitHandler);
            creditLimitHandler.SetNext(creditComissionHandler);
            creditComissionHandler.SetNext(debitInterestRateHandler);
            creditComissionHandler.HandleRequest(choice!, bankBuilder);
            bank.ComissionRate = bankBuilder.ComissionRate.GetValueOrDefault();
            bank.CreditLimit = bankBuilder.CreditLimit.GetValueOrDefault();
            bank.DebitInterestRate = bankBuilder.DebitInterestRate.GetValueOrDefault();
            bank.TransferLimit = bankBuilder.TransferLimit.GetValueOrDefault();
            bank.DepositSpan = bankBuilder.DepositSpan.GetValueOrDefault();
            bank.InterestRateStrategy = bankBuilder.InterestRateStrategy!;
        }

        base.HandleRequest(command);
    }
}
