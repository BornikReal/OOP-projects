using Banks.BankAccounts;

namespace Banks.Persons;

public interface IPerson
{
    PersonStatus Status { get; }
    IEnumerable<IBankAccount> Accounts { get; }
    string Name { get; }
    string Surname { get; }
    string? Adress { get; }
    string? PassportData { get; }
}
