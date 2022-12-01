using Banks.DateObservers;
using Banks.Models;

namespace Banks.BankAccounts;

public class DebitAccount : IBankAccount, IDateObserver
{
    private readonly decimal _interestRate;
    private decimal _interestBalance;
    private Action? _balanceChange;
    public DebitAccount(decimal interestRate, decimal transferLimit, IPerson person)
    {
        _interestBalance = 0;
        _interestRate = interestRate;
        TransferLimit = transferLimit;
        Person = person;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Balance { get; private set; } = 0;
    public decimal TransferLimit { get; }
    public IPerson Person { get; }

    public void Cancel()
    {
        new Action(_balanceChange!)();
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

    public void UpdateNewDay()
    {
        _interestBalance += _interestRate / 365 * Balance;
    }

    public void UpdateNewMonth()
    {
        Balance += _interestBalance;
        _interestBalance = 0;
    }

    public void Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }

        if (amount > Balance)
        {
            throw new ArgumentException("Not enough money on the account");
        }

        if (Person.Status == PersonStatus.Unverified && amount > TransferLimit)
        {
            throw new ArgumentException("Amount must be less than transfer limit for unverified person");
        }

        Balance -= amount;
        _balanceChange = () => Balance += amount;
    }
}
