using Banks.DateObservers;
using Banks.InterestRateStrategy;
using Banks.Models;

namespace Banks.BankBuilders;

public interface IBankBuilder
{
    IBankBuilder SetDebitInterestRate(decimal debitInterestRate);
    IBankBuilder SetInterestRateStrategy(IInterestRateStrategy rateStrategy);
    IBankBuilder SetDepositSpan(TimeSpan depositSpan);
    IBankBuilder SetComissionRate(decimal comissionRate);
    IBankBuilder SetCreditLimit(decimal creditLimit);
    IBankBuilder SetTransferLimit(decimal transferLimit);
    IBankBuilder SetClock(IClock clock);
    public Bank Build();
}
