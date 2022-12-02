using Banks.DateObservers;
using Banks.Models;
using Banks.Notificators;

namespace Banks.BankAccounts;

public class DepositAccount : IBankAccount
{
    private decimal _interestBalance;
    private Action? _balanceChange;
    private Action? _unclock;
    public DepositAccount(decimal balance, decimal interestRate, TimeSpan span, IClock clock, decimal transferLimit, IPerson person)
    {
        _interestBalance = 0;
        InterestRate = interestRate;
        Balance = balance;
        TransferLimit = transferLimit;
        Person = person;
        DepositSpan = span;
        Clock = clock;
        Clock.Subscribe(TimeSpan.FromDays(1), IncreaseInterestSum);
        Clock.Subscribe(TimeSpan.FromDays(30), DepositInterestSum);

        void Unlock()
        {
            IsLocked = false;
            Clock.Unsubscribe(DepositSpan, Unlock);
        }

        _unclock = () => Clock.Unsubscribe(DepositSpan, Unlock);

        Clock.Subscribe(DepositSpan, Unlock);
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Balance { get; private set; }
    public decimal InterestRate { get; }
    public TimeSpan DepositSpan { get; private set; }
    public decimal TransferLimit { get; }
    public IPerson Person { get; }
    public bool IsLocked { get; private set; } = true;
    public INotificatorStrategy? ClienNotificator { get; set; }
    public IClock Clock { get; }

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

    public void Update(Bank bank)
    {
        if (DepositSpan != bank.DepositSpan)
        {
            DepositSpan = bank.DepositSpan;
            _unclock?.Invoke();

            void Unlock()
            {
                IsLocked = false;
                Clock.Unsubscribe(DepositSpan, Unlock);
            }

            _unclock = () => Clock.Unsubscribe(DepositSpan, Unlock);

            Clock.Subscribe(DepositSpan, Unlock);
            ClienNotificator?.Notify("Deposit span was updated by bank");
        }
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
