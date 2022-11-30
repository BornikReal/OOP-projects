namespace Banks.BankAccounts;

public class CreditAccount : IBankAccount
{
    public CreditAccount(decimal comissionRate, decimal creditLimit)
    {
        ComissionRate = comissionRate;
        CreditLimit = creditLimit;
    }

    public decimal Balance { get; private set; } = 0;
    public decimal InterestRate => 0;
    public decimal ComissionRate { get; }
    public decimal CreditLimit { get; }

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }

        if (Balance + amount < CreditLimit)
        {
            throw new ArgumentException("Credit limit exceeded");
        }

        Balance -= amount;
    }
}
