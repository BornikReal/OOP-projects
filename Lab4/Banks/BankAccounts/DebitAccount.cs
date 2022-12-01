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

    public Action Cancel()
    {
        return new Action(_balanceChange!);
    }

    public bool CanDeposit(decimal amount)
    {
        return !(amount < 0 || (Person.Status == PersonStatus.Unverified && amount > TransferLimit));
    }

    public bool CanWithdraw(decimal amount)
    {
        return !(amount < 0 || amount > Balance || (Person.Status == PersonStatus.Unverified && amount > TransferLimit));
    }

    public void Deposit(decimal amount)
    {
        if (!CanDeposit(amount))
            throw new InvalidOperationException("Cannot deposit");

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
        if (!CanWithdraw(amount))
            throw new InvalidOperationException("Cannot withdraw");

        Balance -= amount;
        _balanceChange = () => Balance += amount;
    }
}
