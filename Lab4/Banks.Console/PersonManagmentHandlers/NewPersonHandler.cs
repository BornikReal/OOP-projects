using Banks.Console.Commands;
using Banks.Console.PersonPartCommands;
using Banks.Models;
using Banks.PersonBuilder;

namespace Banks.Console.PersonManagmentHandlers;

public class NewPersonHandler : BaseHandler
{
    public override void HandleRequest(string command)
    {
        if (command == "1")
        {
            System.Console.WriteLine("Creating a new person");
            var personNameHandler = new PersonNameHandle();
            var personSurnameHandler = new PersonSurnameHandler();
            var personAddressHandler = new PersonAddressHandler();
            var personPassportHandler = new PersonPassportHandler();
            personNameHandler.SetNext(personSurnameHandler)
                .SetNext(personAddressHandler)
                .SetNext(personPassportHandler);
            var personBuilder = new DefaultPersonBuilder();
            personNameHandler.HandleRequest(null, personBuilder);
            IPerson person = personBuilder.Build();
            CentralBank.RegisterPerson(person);
            System.Console.WriteLine($"Created person with id {person.Id}");
        }

        base.HandleRequest(command);
    }
}
