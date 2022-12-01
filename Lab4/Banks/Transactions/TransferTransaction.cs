using Banks.BankTransactions;

namespace Banks.Transactions;

public class TransferTransaction : IAccountTransaction
{
    public TransferTransaction(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        WithdrawTransaction = new WithdrawTransaction(fromAccountId, amount);
        DepositTransaction = new DepositTransaction(toAccountId, amount);
        Amount = amount;
    }

    public WithdrawTransaction WithdrawTransaction { get; }
    public DepositTransaction DepositTransaction { get; }
    public decimal Amount { get; }
    public Guid TransactionId { get; } = Guid.NewGuid();
    public TransactionStatus Status { get; private set; } = TransactionStatus.Pending;

    public void Accept(ITransactionVisitor visitor)
    {
        visitor.Visit(this);
        Status = TransactionStatus.Completed;
    }
}
