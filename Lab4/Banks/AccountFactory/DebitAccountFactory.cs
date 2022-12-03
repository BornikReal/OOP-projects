using Banks.BankAccounts;
using Banks.Models;

namespace Banks.AccountBuilders;

public class DebitAccountFactory : IBankAccountFactory
{
    private readonly IPerson _person;

    public DebitAccountFactory(IPerson person)
    {
        _person = person;
    }

    public IBankAccount CreateAccount(Bank bank)
    {
        return new DebitAccount(bank.DebitInterestRate, bank.TransferLimit, _person, bank.Clock);
    }
}
