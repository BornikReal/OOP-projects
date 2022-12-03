using Banks.BankTransactions;

namespace Banks.Transactions;

public class DepositTransaction : IAccountTransaction
{
    public DepositTransaction(Guid accountId, decimal amount)
    {
        AccountId = accountId;
        Amount = amount;
    }

    public Guid AccountId { get; }
    public decimal Amount { get; }
    public Guid TransactionId { get; } = Guid.NewGuid();

    public void Accept(ITransactionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
