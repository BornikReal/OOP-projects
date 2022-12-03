using Banks.BankBuilders;

namespace Banks.Console.BankPartCommands;

public class TransferLimitHandler : BankPartHandler
{
    public override void HandleRequest(string? command, IBankBuilder bankBuilder)
    {
        if (command is null or "4")
        {
            decimal result;
            while (true)
            {
                System.Console.Write("Enter the bank's transfer limit for unverified users: ");
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

            bankBuilder.SetTransferLimit(result);
        }

        base.HandleRequest(command, bankBuilder);
    }
}
