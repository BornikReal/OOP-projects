using Banks.InterestRateStrategy;
using Banks.Models;

namespace Banks.BankBuilders;

public interface IBankBuilder
{
    public IBankBuilder SetDebitInterestRate(decimal debitInterestRate);
    public IBankBuilder SetInterestRateStrategy(IInterestRateStrategy rateStrategy);
    public IBankBuilder SetDepositSpan(TimeSpan depositSpan);
    public IBankBuilder SetComissionRate(decimal comissionRate);
    public IBankBuilder SetCreditLimit(decimal creditLimit);
    public Bank Build();
}
