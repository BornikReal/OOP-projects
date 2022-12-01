using Banks.Transactions;

namespace Banks.BankTransactions;

public interface ITransactionVisitor
{
    BankTransaction? BankTransaction { get; }
    void Visit(DepositTransaction transaction);
    void Visit(WithdrawTransaction transaction);
    void Visit(TransferTransaction transaction);
}
