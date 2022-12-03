using Banks.DateObservers;
using Banks.InterestRateStrategy;
using Banks.Models;

namespace Banks.BankBuilders;

public class DefaultBankBuilder : IBankBuilder
{
    public DefaultBankBuilder()
    { }

    public DefaultBankBuilder(Bank bank)
    {
        Clock = bank.Clock;
        DebitInterestRate = bank.DebitInterestRate;
        InterestRateStrategy = bank.InterestRateStrategy;
        DepositSpan = bank.DepositSpan;
        ComissionRate = bank.ComissionRate;
        CreditLimit = bank.CreditLimit;
        TransferLimit = bank.TransferLimit;
    }

    public IClock? Clock { get; private set; }
    public decimal? DebitInterestRate { get; private set; }
    public IInterestRateStrategy? InterestRateStrategy { get; private set; }
    public TimeSpan? DepositSpan { get; private set; }
    public decimal? ComissionRate { get; private set; }
    public decimal? CreditLimit { get; private set; }
    public decimal? TransferLimit { get; private set; }

    public IBankBuilder SetComissionRate(decimal comissionRate)
    {
        ComissionRate = comissionRate;
        return this;
    }

    public IBankBuilder SetCreditLimit(decimal creditLimit)
    {
        CreditLimit = creditLimit;
        return this;
    }

    public IBankBuilder SetDebitInterestRate(decimal debitInterestRate)
    {
        DebitInterestRate = debitInterestRate;
        return this;
    }

    public IBankBuilder SetDepositSpan(TimeSpan depositSpan)
    {
        DepositSpan = depositSpan;
        return this;
    }

    public IBankBuilder SetInterestRateStrategy(IInterestRateStrategy rateStrategy)
    {
        InterestRateStrategy = rateStrategy;
        return this;
    }

    public IBankBuilder SetTransferLimit(decimal transferLimit)
    {
        TransferLimit = transferLimit;
        return this;
    }

    public IBankBuilder SetClock(IClock clock)
    {
        Clock = clock;
        return this;
    }

    public Bank Build()
    {
        if (Clock == null || DebitInterestRate == null || InterestRateStrategy == null || DepositSpan == null || ComissionRate == null || CreditLimit == null || TransferLimit == null)
            throw new Exception();
        return new Bank(Clock, DebitInterestRate.GetValueOrDefault(), InterestRateStrategy, ComissionRate.GetValueOrDefault(), CreditLimit.GetValueOrDefault(), TransferLimit.GetValueOrDefault(), DepositSpan.GetValueOrDefault());
    }
}
