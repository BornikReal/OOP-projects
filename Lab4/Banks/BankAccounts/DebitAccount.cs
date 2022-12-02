using Banks.DateObservers;
using Banks.Models;

namespace Banks.BankAccounts;

public class DebitAccount : IBankAccount
{
    private readonly decimal _interestRate;
    private decimal _interestBalance;
    private Action? _balanceChange;
    public DebitAccount(decimal interestRate, decimal transferLimit, IPerson person, IClock clock)
    {
        _interestBalance = 0;
        _interestRate = interestRate;
        TransferLimit = transferLimit;
        Person = person;
        clock.TimeSpans[TimeSpan.FromDays(1)] += IncreaseInterestSum;
        clock.TimeSpans[TimeSpan.FromDays(30)] += DepositInterestSum;
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

    public void Withdraw(decimal amount)
    {
        if (!CanWithdraw(amount))
            throw new InvalidOperationException("Cannot withdraw");

        Balance -= amount;
        _balanceChange = () => Balance += amount;
    }

    private void IncreaseInterestSum()
    {
        _interestBalance += _interestRate / 365 * Balance;
    }

    private void DepositInterestSum()
    {
        Balance += _interestBalance;
        _interestBalance = 0;
    }
}
