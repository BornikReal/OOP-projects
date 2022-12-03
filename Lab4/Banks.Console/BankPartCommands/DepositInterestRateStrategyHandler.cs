using Banks.BankBuilders;
using Banks.InterestRateStrategy;

namespace Banks.Console.BankPartCommands;

public class DepositInterestRateStrategyHandler : BankPartHandler
{
    public override void HandleRequest(string? command, IBankBuilder bankBuilder)
    {
        if (command is null or "6")
        {
            var interests = new Dictionary<decimal, decimal>();
            string? input;
            decimal sum, rate;
            while (true)
            {
                while (true)
                {
                    System.Console.Write("Enter the amount to which the interest rate will be set: ");
                    input = System.Console.ReadLine();

                    try
                    {
                        sum = Convert.ToDecimal(input);
                        break;
                    }
                    catch (OverflowException)
                    {
                        System.Console.WriteLine($"{input} is not a number!");
                    }
                }

                while (true)
                {
                    System.Console.Write("Enter the interest rate: ");
                    input = System.Console.ReadLine();
                    try
                    {
                        rate = Convert.ToDecimal(input);
                        break;
                    }
                    catch (OverflowException)
                    {
                        System.Console.WriteLine($"{input} is not a number!");
                    }
                }

                interests.Add(sum, rate);
                System.Console.Write("Stop(y/n)?");
                input = System.Console.ReadLine();
                if (input == "y")
                    break;
            }

            bankBuilder.SetInterestRateStrategy(new DefaultInterestRateStrategy(interests));
        }

        base.HandleRequest(command, bankBuilder);
    }
}
