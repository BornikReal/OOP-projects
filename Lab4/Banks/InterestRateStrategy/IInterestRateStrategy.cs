namespace Banks.InterestRateStrategy;

public interface IInterestRateStrategy
{
    decimal CalculateInterestRate(decimal balance);
}
