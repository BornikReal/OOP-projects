using Banks.BankAccounts;
using Banks.Models;

namespace Banks.AccountBuilders;

public class CreditAccountFactory : IBankAccountFactory
{
    private readonly IPerson _person;

    public CreditAccountFactory(IPerson person)
    {
        _person = person;
    }

    public IBankAccount CreateAccount(Bank bank)
    {
        return new CreditAccount(bank.ComissionRate, bank.CreditLimit, bank.TransferLimit, _person);
    }
}
