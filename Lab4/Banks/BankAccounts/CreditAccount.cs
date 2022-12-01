using Banks.Models;

namespace Banks.BankAccounts;

public class CreditAccount : IBankAccount
{
    private Action? _balanceChange;
    public CreditAccount(decimal comissionRate, decimal creditLimit, decimal transferLimit, IPerson person)
    {
        ComissionRate = comissionRate;
        CreditLimit = creditLimit;
        TransferLimit = transferLimit;
        Person = person;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Balance { get; private set; } = 0;
    public decimal ComissionRate { get; }
    public decimal CreditLimit { get; }
    public decimal TransferLimit { get; }
    public IPerson Person { get; }

    public void Cancel()
    {
        _balanceChange!();
    }

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }

        if (Person.Status == PersonStatus.Unverified && amount > TransferLimit)
        {
            throw new ArgumentException("Amount must be less than transfer limit for unverified person");
        }

        Balance += amount;
        _balanceChange = () => Balance -= amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }

        if (Person.Status == PersonStatus.Unverified && amount > TransferLimit)
        {
            throw new ArgumentException("Amount must be less than transfer limit for unverified person");
        }

        switch (Balance)
        {
            case var balance when balance >= 0:
                Balance -= amount;
                _balanceChange = () => Balance += amount;
                break;
            case var balance when balance >= CreditLimit:
                decimal withdrawAmount = amount + ComissionRate;
                Balance -= withdrawAmount;
                _balanceChange = () => Balance += withdrawAmount;
                break;
            default:
                throw new InvalidOperationException("Unexpected balance value");
        }
    }
}
