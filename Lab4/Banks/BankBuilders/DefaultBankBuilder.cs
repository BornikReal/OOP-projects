using Banks.DateObservers;
using Banks.InterestRateStrategy;
using Banks.Models;

namespace Banks.BankBuilders;

public class DefaultBankBuilder : IBankBuilder
{
    private IClock? _clock;
    private decimal? _debitInterestRate;
    private IInterestRateStrategy? _interestRateStrategy;
    private TimeSpan? _depositSpan;
    private decimal? _comissionRate;
    private decimal? _creditLimit;
    private decimal? _transferLimit;

    public IBankBuilder SetComissionRate(decimal comissionRate)
    {
        _comissionRate = comissionRate;
        return this;
    }

    public IBankBuilder SetCreditLimit(decimal creditLimit)
    {
        _creditLimit = creditLimit;
        return this;
    }

    public IBankBuilder SetDebitInterestRate(decimal debitInterestRate)
    {
        _debitInterestRate = debitInterestRate;
        return this;
    }

    public IBankBuilder SetDepositSpan(TimeSpan depositSpan)
    {
        _depositSpan = depositSpan;
        return this;
    }

    public IBankBuilder SetInterestRateStrategy(IInterestRateStrategy rateStrategy)
    {
        _interestRateStrategy = rateStrategy;
        return this;
    }

    public IBankBuilder SetTransferLimit(decimal transferLimit)
    {
        _transferLimit = transferLimit;
        return this;
    }

    public IBankBuilder SetClock(IClock clock)
    {
        _clock = clock;
        return this;
    }

    public Bank Build()
    {
        if (_clock == null || _debitInterestRate == null || _interestRateStrategy == null || _depositSpan == null || _comissionRate == null || _creditLimit == null || _transferLimit == null)
            throw new Exception();
        return new Bank(_clock, _debitInterestRate.GetValueOrDefault(), _interestRateStrategy, _comissionRate.GetValueOrDefault(), _creditLimit.GetValueOrDefault(), _transferLimit.GetValueOrDefault(), _depositSpan.GetValueOrDefault());
    }
}
