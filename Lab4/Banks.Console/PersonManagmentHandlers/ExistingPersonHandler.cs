using Banks.Console.Commands;
using Banks.Console.PersonPartCommands;
using Banks.Models;
using Banks.PersonBuilder;

namespace Banks.Console.PersonManagmentHandlers;

public class ExistingPersonHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "2")
        {
            Guid result;
            while (true)
            {
                System.Console.WriteLine("Enter person id:");
                string? input = System.Console.ReadLine();
                try
                {
                    result = Guid.Parse(input!);
                    break;
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine($"{input} is not a number!");
                }
            }

            IPerson person = CentralBank.GetPerson(result);
            var personBuilder = new DefaultPersonBuilder(person);
            System.Console.WriteLine("Choose option to edit");
            System.Console.WriteLine("Options:");
            System.Console.WriteLine("1. Name");
            System.Console.WriteLine("2. Surname");
            System.Console.WriteLine("3. Address");
            System.Console.WriteLine("4. Passport");
            System.Console.Write("Your choice: ");
            string? choice = System.Console.ReadLine();
            var personNameHandler = new PersonNameHandle();
            var personSurnameHandler = new PersonSurnameHandler();
            var personAddressHandler = new PersonAddressHandler();
            var personPassportHandler = new PersonPassportHandler();
            personNameHandler.SetNext(personSurnameHandler);
            personSurnameHandler.SetNext(personAddressHandler);
            personAddressHandler.SetNext(personPassportHandler);
            personNameHandler.HandleRequest(choice, personBuilder);
            person.Name = personBuilder.Name!;
            person.Surname = personBuilder.Surname!;
            person.Adress = personBuilder.Adress!;
            person.PassportData = personBuilder.PassportData!;
        }

        base.HandleRequest(command);
    }
}
