using Banks.DateObservers;
using Banks.Models;
using Banks.Notificators;

namespace Banks.BankAccounts;

public class DebitAccount : IBankAccount
{
    private Action? _balanceChange;
    public DebitAccount(decimal interestRate, decimal transferLimit, IPerson person, IClock clock)
    {
        InterestBalance = 0;
        InterestRate = interestRate;
        TransferLimit = transferLimit;
        Person = person;
        clock.Subscribe(TimeSpan.FromDays(1), IncreaseInterestSum);
        clock.Subscribe(TimeSpan.FromDays(30), DepositInterestSum);
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal InterestBalance { get; private set; }
    public decimal InterestRate { get; private set; }
    public decimal Balance { get; private set; } = 0;
    public decimal TransferLimit { get; }
    public IPerson Person { get; }
    public INotificatorStrategy? ClienNotificator { get; set; }

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

    public void Update(Bank bank)
    {
        if (InterestRate != bank.DebitInterestRate)
        {
            InterestRate = bank.DebitInterestRate;
            ClienNotificator?.Notify("Interest rate was updated by bank");
        }
    }

    private void IncreaseInterestSum()
    {
        InterestBalance += InterestRate / 365 * Balance;
    }

    private void DepositInterestSum()
    {
        Balance += InterestBalance;
        InterestBalance = 0;
    }
}
