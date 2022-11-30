using Banks.BankAccounts;

namespace Banks.Models;

public class Person : IPerson
{
    public Person(string name, string surname, string? adress, string? passportData)
    {
        Name = name;
        Surname = surname;
        Adress = adress;
        PassportData = passportData;
    }

    public PersonStatus Status => (Adress == null || PassportData == null) ? PersonStatus.Unverified : PersonStatus.Verified;
    public string Name { get; }
    public string Surname { get; }
    public string? Adress { get; }
    public string? PassportData { get; }

    public IEnumerable<IBankAccount> Accounts => throw new NotImplementedException();
}
