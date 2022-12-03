using Banks.Console.Commands;
using Banks.Models;

namespace Banks.Console.MenuOptionsCommands;

public class MadeInHeavenHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "5")
        {
            int result;
            while (true)
            {
                System.Console.WriteLine("Enter number of days:");
                string? input = System.Console.ReadLine();
                try
                {
                    result = Convert.ToInt32(input);
                    break;
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine($"{input} is not a number!");
                }
            }

            CentralBank.AddDays(result);
        }

        base.HandleRequest(command);
    }
}