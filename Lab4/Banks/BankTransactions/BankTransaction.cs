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
    public void CancelTransaction()
    {
        _cancelTransaction();
    }
}
