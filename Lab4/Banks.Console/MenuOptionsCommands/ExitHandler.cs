using Banks.Console.Commands;

namespace Banks.Console.MenuOptionsCommands;

public class ExitHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "6")
        {
            System.Console.WriteLine("Arrivederci!");
            Environment.Exit(0);
        }
        else
        {
            base.HandleRequest(command);
        }
    }
}
