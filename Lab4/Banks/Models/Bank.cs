using Banks.AccountBuilders;
using Banks.BankAccounts;
using Banks.DateObservers;
using Banks.InterestRateStrategy;

namespace Banks.Models;

public class Bank
{
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();
    private decimal _debitInterestRate;

    public Bank(IClock clock, decimal debitInterestRate, IInterestRateStrategy strategy, TimeSpan depositSpan, decimal commisionRate, decimal creditLimit, decimal transferLimit)
    {
        Clock = clock;
        DebitInterestRate = debitInterestRate;
        InterestRateStrategy = strategy;
        DepositSpan = depositSpan;
        ComissionRate = commisionRate;
        CreditLimit = creditLimit;
        TransferLimit = transferLimit;
    }

    public delegate void AccountHandler(IBankAccount account);
    public event AccountHandler? Notify;
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
    public decimal TransferLimit { get; set; }
    public IClock Clock { get; }

    public void CreateBankAccount(IBankAccountFactory accountFactory)
    {
        IBankAccount account = accountFactory.CreateAccount(this);
        Notify?.Invoke(account);
        _accounts.Add(account);
    }
}
