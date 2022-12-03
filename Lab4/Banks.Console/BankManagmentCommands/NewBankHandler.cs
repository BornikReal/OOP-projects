using Banks.BankBuilders;
using Banks.Console.BankPartCommands;
using Banks.Console.Commands;
using Banks.Models;

namespace Banks.Console.BankManagmentCommands;

public class NewBankHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "1")
        {
            System.Console.WriteLine("Creating a new bank");
            var creditComissionHandler = new CreditComissionHandler();
            var creditLimitHandler = new CreditLimitHandler();
            var debitInterestRateHandler = new DebitInterestRateHandler();
            var transferLimitHandler = new TransferLimitHandler();
            var depositAccountSpanHandler = new DepositAccountSpanHandler();
            var depositInterestRateStrategyHandler = new DepositInterestRateStrategyHandler();
            creditComissionHandler.SetNext(depositInterestRateStrategyHandler)
                .SetNext(depositAccountSpanHandler)
                .SetNext(transferLimitHandler)
                .SetNext(creditLimitHandler)
                .SetNext(creditComissionHandler)
                .SetNext(debitInterestRateHandler);
            var bankBuilder = new DefaultBankBuilder();
            creditComissionHandler.HandleRequest(null, bankBuilder);
            System.Console.WriteLine($"Created bank with id {CentralBank.CreateBank(bankBuilder).Id}");
        }

        base.HandleRequest(command);
    }
}
