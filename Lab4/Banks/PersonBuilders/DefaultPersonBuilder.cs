using Banks.Models;

namespace Banks.PersonBuilder;

public class DefaultPersonBuilder : IPersonBuilder
{
    public DefaultPersonBuilder()
    { }
    public DefaultPersonBuilder(IPerson person)
    {
        Name = person.Name;
        Surname = person.Surname;
        Adress = person.Adress;
        PassportData = person.PassportData;
    }

    public string? Name { get; private set; }
    public string? Surname { get; private set; }
    public string? Adress { get; private set; }
    public string? PassportData { get; private set; }

    public IPersonBuilder SetName(string name)
    {
        Name = name;
        return this;
    }

    public IPersonBuilder SetSurname(string surname)
    {
        Surname = surname;
        return this;
    }

    public IPersonBuilder SetAdress(string adress)
    {
        Adress = adress;
        return this;
    }

    public IPersonBuilder SetPassportData(string phone)
    {
        PassportData = phone;
        return this;
    }

    public IPerson Build()
    {
        if (Name == null || Surname == null)
            throw new Exception();
        return new Person(Name, Surname, Adress, PassportData);
    }
}
