namespace Banks.BankAccounts;

public class CreditAccount : IBankAccount
{
    public CreditAccount(decimal comissionRate, decimal creditLimit, decimal transferLimit)
    {
        ComissionRate = comissionRate;
        CreditLimit = creditLimit;
        TransferLimit = transferLimit;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Balance { get; private set; } = 0;
    public decimal ComissionRate { get; }
    public decimal CreditLimit { get; }
    public decimal TransferLimit { get; }

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

        Balance -= Balance switch
        {
            var b when b >= 0 => amount + ComissionRate,
            var b when b >= CreditLimit => amount,
            _ => throw new ArgumentException("Not enough money on the account"),
        };
    }
}
