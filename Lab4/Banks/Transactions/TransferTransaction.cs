using Banks.BankTransactions;

namespace Banks.Transactions;

public class TransferTransaction : IAccountTransaction
{
    public TransferTransaction(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
    }

    public Guid FromAccountId { get; }
    public Guid ToAccountId { get; }
    public decimal Amount { get; }
    public Guid TransactionId { get; } = Guid.NewGuid();

    public void Accept(ITransactionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
