using Banks.Transactions;

namespace Banks.BankTransactions;

public interface ITransactionVisitor
{
    void Visit(DepositTransaction transaction);
    void Visit(WithdrawTransaction transaction);
    void Visit(TransferTransaction transaction);
}
