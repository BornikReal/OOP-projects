using Banks.BankBuilders;

namespace Banks.Console.BankPartCommands;

public class DebitInterestRateHandler : BankPartHandler
{
    public override void HandleRequest(string? command, IBankBuilder bankBuilder)
    {
        if (command is null or "3")
        {
            decimal result;
            while (true)
            {
                System.Console.Write("Enter the bank's debit interest rate: ");
                string? input = System.Console.ReadLine();
                try
                {
                    result = Convert.ToDecimal(input);
                    break;
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine($"{input} is not a number!");
                }
            }

            bankBuilder.SetDebitInterestRate(result);
        }

        base.HandleRequest(command, bankBuilder);
    }
}