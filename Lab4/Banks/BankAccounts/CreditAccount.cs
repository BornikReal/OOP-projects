using Banks.Models;
using Banks.Notificators;

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
    public decimal ComissionRate { get; private set; }
    public decimal CreditLimit { get; }
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
        return !(amount < 0 || (Person.Status == PersonStatus.Unverified && amount > TransferLimit) || Balance - amount < CreditLimit);
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

        switch (Balance)
        {
            case var balance when balance >= 0:
                Balance -= amount;
                _balanceChange = () => Balance += amount;
                break;
            case var balance when balance - amount >= CreditLimit:
                decimal withdrawAmount = amount + ComissionRate;
                Balance -= withdrawAmount;
                _balanceChange = () => Balance += withdrawAmount;
                break;
        }
    }

    public void Update(Bank bank)
    {
        if (ComissionRate != bank.ComissionRate)
        {
            ComissionRate = bank.ComissionRate;
            ClienNotificator?.Notify("Comission rate was updated by bank");
        }
    }
}
