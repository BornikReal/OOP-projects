using Banks.DateObservers;

namespace Banks.BankAccounts;

public class DebitAccount : IBankAccount, IDateObserver
{
    private readonly decimal _interestRate;
    private decimal _interestBalance;
    public DebitAccount(decimal interestRate, decimal transferLimit)
    {
        _interestBalance = 0;
        _interestRate = interestRate;
        TransferLimit = transferLimit;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Balance { get; private set; } = 0;
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

        Balance -= amount;
    }
}
