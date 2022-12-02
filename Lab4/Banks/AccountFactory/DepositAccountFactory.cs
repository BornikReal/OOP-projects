using Banks.BankAccounts;
using Banks.Models;

namespace Banks.AccountBuilders;

public class DepositAccountFactory : IBankAccountFactory
{
    private readonly decimal _balance;
    private readonly IPerson _person;

    public DepositAccountFactory(decimal balance, IPerson person)
    {
        _balance = balance;
        _person = person;
    }

    public IBankAccount CreateAccount(Bank bank)
    {
        decimal interestRate = bank.InterestRateStrategy.CalculateInterestRate(_balance);
        if (interestRate is < 0 or > 100)
            throw new Exception();
        return new DepositAccount(_balance, interestRate, bank.DepositSpan, bank.Clock, bank.TransferLimit, _person);
    }
}
