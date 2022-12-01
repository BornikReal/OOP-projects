using Banks.BankAccounts;
using Banks.Transactions;

namespace Banks.BankTransactions;

public class DefaultTransactionVisitor : ITransactionVisitor
{
    public DefaultTransactionVisitor(IBankAccount account)
    {
        Account = account;
    }

    public IBankAccount Account { get; set; }
    public BankTransaction? BankTransaction { get; set; }
    public void Visit(DepositTransaction transaction)
    {
        Account.Deposit(transaction.Amount);
        BankTransaction = new BankTransaction(transaction, () => Account.Withdraw(transaction.Amount));
    }

    public void Visit(WithdrawTransaction transaction)
    {
        Account.Withdraw(transaction.Amount);
        BankTransaction = new BankTransaction(transaction, () => Account.Deposit(transaction.Amount));
    }

    public void Visit(TransferTransaction transaction)
    {
    }
}
