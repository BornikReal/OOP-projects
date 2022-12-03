using Banks.AccountBuilders;
using Banks.BankAccounts;
using Banks.DateObservers;
using Banks.InterestRateStrategy;
using Banks.Notificators;

namespace Banks.Models;

public class Bank
{
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();
    private decimal _debitInterestRate;
    private IInterestRateStrategy _interestRateStrategy;
    private decimal _comissionRate;
    private decimal _creditLimit;
    private decimal _transferLimit;
    private TimeSpan _depositSpan;

    public Bank(IClock clock, decimal debitInterestRate, IInterestRateStrategy strategy, decimal commisionRate, decimal creditLimit, decimal transferLimit, TimeSpan depositSpan)
    {
        Clock = clock;
        DebitInterestRate = debitInterestRate;
        _interestRateStrategy = strategy;
        ComissionRate = commisionRate;
        CreditLimit = creditLimit;
        TransferLimit = transferLimit;
        DepositSpan = depositSpan;
    }

    public delegate void AccountHandler(IBankAccount account);
    public event AccountHandler? Notify;
    public Guid Id { get; } = Guid.NewGuid();
    public decimal DebitInterestRate
    {
        get => _debitInterestRate;
        set
        {
            if (value is < 0 or > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Interest rate must be between 0 and 100");

            _debitInterestRate = value;
            TriggerAccount();
        }
    }

    public IInterestRateStrategy InterestRateStrategy
    {
        get => _interestRateStrategy;
        set
    {
            _interestRateStrategy = value;
            TriggerAccount();
        }
    }

    public decimal ComissionRate
    {
        get => _comissionRate;
        set
        {
            if (value is < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Comission rate must be upper 0");

            _comissionRate = value;
            TriggerAccount();
        }
    }

    public decimal CreditLimit
    {
        get => _creditLimit;
        set
        {
            if (value is > 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Comission rate must be below 0");

            _creditLimit = value;
            TriggerAccount();
        }
    }

    public decimal TransferLimit
    {
        get => _transferLimit;
        set
        {
            if (value is < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Comission rate must be upper then 0");

            _transferLimit = value;
            TriggerAccount();
        }
    }

    public TimeSpan DepositSpan
    {
        get => _depositSpan;
        set
        {
            _depositSpan = value;
            TriggerAccount();
        }
    }

    public IClock Clock { get; }

    public Guid CreateBankAccount(IBankAccountFactory accountFactory)
    {
        IBankAccount account = accountFactory.CreateAccount(this);
        Notify?.Invoke(account);
        _accounts.Add(account);
        return account.Id;
    }

    public void SubcribeAccountToBankChanges(Guid accountId, INotificatorStrategy strategy)
    {
        IBankAccount? account = _accounts.FirstOrDefault(x => x.Id == accountId);
        if (account is null)
            throw new InvalidOperationException("Account not found");
        account.ClienNotificator = strategy;
    }

    private void TriggerAccount()
    {
        foreach (IBankAccount account in _accounts)
        {
            account.Update(this);
        }
    }
}
