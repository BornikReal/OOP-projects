using Banks.BankTransactions;

namespace Banks.Transactions;

public interface IAccountTransaction
{
    TransactionStatus Status { get; }
    Guid TransactionId { get; }
    decimal Amount { get; }
    void Accept(ITransactionVisitor visitor);
}
