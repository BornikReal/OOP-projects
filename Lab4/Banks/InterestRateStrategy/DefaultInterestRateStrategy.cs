namespace Banks.InterestRateStrategy;

public class DefaultInterestRateStrategy : IInterestRateStrategy
{
    private readonly Dictionary<decimal, decimal> _interests;

    public DefaultInterestRateStrategy(Dictionary<decimal, decimal> interests)
    {
        _interests = interests;
    }

    public decimal CalculateInterestRate(decimal balance)
    {
        return _interests.MinBy(x => x.Key >= balance ? x.Value : 0).Value;
    }
}
