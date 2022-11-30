using Banks.DateObservers;

namespace Banks.BankAccounts;

public class DebitAccount : IBankAccount, IDateObserver
{
    private decimal _interestBalance;
    private DateTime _curDate;
    public DebitAccount(decimal interestRate)
    {
        InterestRate = interestRate;
    }

    public decimal Balance { get; private set; } = 0;
    public decimal InterestRate { get; }
    public decimal ComissionRate => 0;

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }

        Balance += amount;
    }

    public void Update(IDateSubject dateSubject)
    {
        if (dateSubject.CurDate.Day != _curDate.Day)
            _interestBalance += InterestRate / 365 * Balance;
        if (dateSubject.CurDate.Month != _curDate.Month)
        {
            Balance += _interestBalance;
            _interestBalance = 0;
        }

        _curDate = dateSubject.CurDate;
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
