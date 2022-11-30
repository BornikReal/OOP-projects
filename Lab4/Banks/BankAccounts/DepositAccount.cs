using Banks.DateObservers;

namespace Banks.BankAccounts;

public class DepositAccount : IBankAccount, IDateObserver
{
    private readonly IDateSubject _dateSubject;
    private decimal _interestBalance;
    public DepositAccount(decimal balance, decimal interestRate, DateTime date, IDateSubject dateSubject, decimal transferLimit)
    {
        _interestBalance = 0;
        InterestRate = interestRate;
        DateOfEnding = date;
        Balance = balance;
        _dateSubject = dateSubject;
        TransferLimit = transferLimit;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Balance { get; private set; }
    public decimal InterestRate { get; }
    public DateTime DateOfEnding { get; }
    public decimal TransferLimit { get; }

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }

        Balance += amount;
    }

    public void UpdateNewDay()
    {
        _interestBalance += InterestRate / 365 * Balance;
    }

    public void UpdateNewMonth()
    {
        Balance += _interestBalance;
        _interestBalance = 0;
    }

    public void Withdraw(decimal amount)
    {
        if (_dateSubject.CurDate < DateOfEnding)
        {
            throw new ArgumentException("You can't withdraw money before the end of the deposit");
        }

        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }

        if (amount > Balance)
        {
            throw new ArgumentException("Not enough money on the account");
        }

        Balance -= amount;
    }
}
