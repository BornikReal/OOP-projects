using Banks.BankAccounts;
using Banks.InterestRateStrategy;

namespace Banks.Models;

public class Bank
{
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();
    private decimal _debitInterestRate;

    public Bank(decimal debitInterestRate, IInterestRateStrategy strategy, TimeSpan depositSpan, decimal commisionRate, decimal creditLimit)
    {
        DebitInterestRate = debitInterestRate;
        InterestRateStrategy = strategy;
        DepositSpan = depositSpan;
        ComissionRate = commisionRate;
        CreditLimit = creditLimit;
    }

    public delegate void AccountHandler(IBankAccount account);
    private event AccountHandler? Notify;

    public decimal DebitInterestRate
    {
        get => _debitInterestRate;
        set
        {
            if (value is < 0 or > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Interest rate must be between 0 and 100");

            _debitInterestRate = value;
        }
    }

    public IInterestRateStrategy InterestRateStrategy { get; set; }

    public TimeSpan DepositSpan { get; set; }
    public decimal ComissionRate { get; set; }
    public decimal CreditLimit { get; set; }

    public void CreateDebitAccount()
    {
        var account = new DebitAccount(_debitInterestRate);
        Notify?.Invoke(account);
        _accounts.Add(account);
    }

    public void CreateDepositAccount(decimal balance)
    {
        decimal interestRate = InterestRateStrategy.CalculateInterestRate(balance);
        if (interestRate is < 0 or > 100)
            throw new Exception();
        var account = new DepositAccount(balance, interestRate, DateTime.Now + DepositSpan);
        Notify?.Invoke(account);
        _accounts.Add(account);
    }

    public void CreateCreditAccount()
    {
        var account = new CreditAccount(ComissionRate, CreditLimit);
        Notify?.Invoke(account);
        _accounts.Add(account);
    }
}
