using Banks.Console.Commands;
using Banks.Console.PersonManagmentHandlers;

namespace Banks.Console.MenuOptionsCommands;

public class ClientManagmentHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "2")
        {
            System.Console.WriteLine("Bank management menu");
            System.Console.WriteLine("Options:");
            System.Console.WriteLine("1. Create new person");
            System.Console.WriteLine("2. Configure an existing person");
            var existingPersonManagment = new ExistingPersonHandler();
            var newPersonManagment = new NewPersonHandler();
            newPersonManagment.SetNext(existingPersonManagment);
            System.Console.Write("Your choice: ");
            newPersonManagment.HandleRequest(System.Console.ReadLine() !);
        }

        base.HandleRequest(command);
    }
}
