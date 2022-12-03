using Banks.BankBuilders;

namespace Banks.Console.BankPartCommands;

public class DepositAccountSpanHandler : BankPartHandler
{
    public override void HandleRequest(string? command, IBankBuilder bankBuilder)
    {
        if (command is null or "5")
        {
            TimeSpan result;
            while (true)
            {
                System.Console.Write("Enter the bank's deposit acoount span: ");
                string? input = System.Console.ReadLine();
                try
                {
                    result = TimeSpan.Parse(input!);
                    break;
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine($"{input} is not a number!");
                }
            }

            bankBuilder.SetDepositSpan(result);
        }

        base.HandleRequest(command, bankBuilder);
    }
}
