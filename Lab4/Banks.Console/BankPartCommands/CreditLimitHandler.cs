using Banks.BankBuilders;

namespace Banks.Console.BankPartCommands;

public class CreditLimitHandler : BankPartHandler
{
    public override void HandleRequest(string? command, IBankBuilder bankBuilder)
    {
        if (command is null or "2")
        {
            decimal result;
            while (true)
            {
                System.Console.Write("Enter the bank's credit limit: ");
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

            bankBuilder.SetCreditLimit(result);
        }

        base.HandleRequest(command, bankBuilder);
    }
}