using Banks.BankTransactions;

namespace Banks.Transactions;

public interface IAccountTransaction
{
    Guid TransactionId { get; }
    decimal Amount { get; }
    void Accept(ITransactionVisitor visitor);
}
