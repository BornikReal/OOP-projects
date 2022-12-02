using Banks.DateObservers;
using Banks.Models;

namespace Banks.BankAccounts;

public class DepositAccount : IBankAccount
{
    private decimal _interestBalance;
    private Action? _balanceChange;
    public DepositAccount(decimal balance, decimal interestRate, TimeSpan span, IClock clock, decimal transferLimit, IPerson person)
    {
        _interestBalance = 0;
        InterestRate = interestRate;
        Balance = balance;
        TransferLimit = transferLimit;
        Person = person;
        clock.TimeSpans[TimeSpan.FromDays(1)] += IncreaseInterestSum;
        clock.TimeSpans[TimeSpan.FromDays(30)] += DepositInterestSum;
        clock.TimeSpans[span] += () => IsLocked = false;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Balance { get; private set; }
    public decimal InterestRate { get; }
    public DateTime DateOfEnding { get; }
    public decimal TransferLimit { get; }
    public IPerson Person { get; }
    public bool IsLocked { get; private set; } = true;

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
        return !(IsLocked || amount < 0 || amount > Balance || (Person.Status == PersonStatus.Unverified && amount > TransferLimit));
    }

    public void Deposit(decimal amount)
    {
        if (!CanDeposit(amount))
            throw new InvalidOperationException("Cannot deposit");

        Balance += amount;
        _balanceChange = () => Balance -= amount;
    }

    public void Withdraw(decimal amount)
    {
        if (!CanWithdraw(amount))
            throw new InvalidOperationException("Cannot withdraw");

        Balance -= amount;
        _balanceChange = () => Balance += amount;
    }

    private void IncreaseInterestSum()
    {
        _interestBalance += InterestRate / 365 * Balance;
    }

    private void DepositInterestSum()
    {
        Balance += _interestBalance;
        _interestBalance = 0;
    }
}
