﻿using Banks.BankBuilders;

namespace Banks.Console.BankPartCommands;

public class CreditComissionHandler : BankPartHandler
{
    public override void HandleRequest(string? command, IBankBuilder bankBuilder)
    {
        if (command is null or "1")
        {
            decimal result;
            while (true)
            {
                System.Console.Write("Enter the bank's credit comission: ");
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

            bankBuilder.SetComissionRate(result);
        }

        base.HandleRequest(command, bankBuilder);
    }
}
