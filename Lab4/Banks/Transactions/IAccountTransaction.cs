using Banks.BankTransactions;

namespace Banks.Transactions;

public interface IAccountTransaction
{
    Guid TransactionId { get; }
    void Accept(ITransactionVisitor visitor);
}
