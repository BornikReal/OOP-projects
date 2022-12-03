using Banks.Transactions;

namespace Banks.BankTransactions;

public class BankTransaction
{
    private readonly Action _cancelTransaction;
    public BankTransaction(IAccountTransaction transaction, Action cancelTransaction)
    {
        Transaction = transaction;
        _cancelTransaction = cancelTransaction;
    }

    public IAccountTransaction Transaction { get; }
    public TransactionStatus Status { get; private set; } = TransactionStatus.Completed;
    public void CancelTransaction()
    {
        if (Status == TransactionStatus.Cancelled)
            throw new InvalidOperationException("Transaction already cancelled");

        _cancelTransaction();
        Status = TransactionStatus.Cancelled;
    }
}
