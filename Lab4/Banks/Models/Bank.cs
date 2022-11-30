using Banks.BankAccounts;
using Banks.DateObservers;
using Banks.InterestRateStrategy;

namespace Banks.Models;

public class Bank
{
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();
    private readonly IDateSubject _dateSubject;
    private decimal _debitInterestRate;

    private Bank(IDateSubject dateSubject, decimal debitInterestRate, IInterestRateStrategy strategy, TimeSpan depositSpan, decimal commisionRate, decimal creditLimit)
    {
        _dateSubject = dateSubject;
        DebitInterestRate = debitInterestRate;
        InterestRateStrategy = strategy;
        DepositSpan = depositSpan;
        ComissionRate = commisionRate;
        CreditLimit = creditLimit;
    }

    public delegate void AccountHandler(IBankAccount account);
    private event AccountHandler? Notify;
    public static BankBuilder Builder { get; } = new BankBuilder();

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

    public void CreateDebitAccount()
    {
        var account = new DebitAccount(_debitInterestRate, TransferLimit);
        Notify?.Invoke(account);
        _accounts.Add(account);
    }

    public void CreateDepositAccount(decimal balance)
    {
        decimal interestRate = InterestRateStrategy.CalculateInterestRate(balance);
        if (interestRate is < 0 or > 100)
            throw new Exception();
        var account = new DepositAccount(balance, interestRate, _dateSubject.CurDate + DepositSpan, _dateSubject, TransferLimit);
        Notify?.Invoke(account);
        _accounts.Add(account);
    }

    public void CreateCreditAccount()
    {
        var account = new CreditAccount(ComissionRate, CreditLimit, TransferLimit);
        Notify?.Invoke(account);
        _accounts.Add(account);
    }

    public class BankBuilder
    {
        private decimal? _debitInterestRate;
        private IInterestRateStrategy? _interestRateStrategy;
        private TimeSpan? _depositSpan;
        private decimal? _comissionRate;
        private decimal? _creditLimit;

        public delegate void BankHandler(Bank bank);
        private event BankHandler? Notify;

        public BankBuilder SetDebitInterestRate(decimal debitInterestRate)
        {
            _debitInterestRate = debitInterestRate;
            return this;
        }

        public BankBuilder SetInterestRateStrategy(IInterestRateStrategy rateStrategy)
        {
            _interestRateStrategy = rateStrategy;
            return this;
        }

        public BankBuilder SetDepositSpan(TimeSpan depositSpan)
        {
            _depositSpan = depositSpan;
            return this;
        }

        public BankBuilder SetComissionRate(decimal comissionRate)
        {
            _comissionRate = comissionRate;
            return this;
        }

        public BankBuilder SetCreditLimit(decimal creditLimit)
        {
            _creditLimit = creditLimit;
            return this;
        }

        public void Reset()
        {
            _debitInterestRate = null;
            _interestRateStrategy = null;
            _depositSpan = null;
            _comissionRate = null;
            _creditLimit = null;
        }

        public Bank Build()
        {
            if (_debitInterestRate == null || _interestRateStrategy == null || _depositSpan == null || _comissionRate == null || _creditLimit == null)
                throw new Exception();

            var bank = new Bank((decimal)_debitInterestRate, _interestRateStrategy, (TimeSpan)_depositSpan, (decimal)_comissionRate, (decimal)_creditLimit);
            Notify?.Invoke(bank);
            return bank;
        }
    }
}
