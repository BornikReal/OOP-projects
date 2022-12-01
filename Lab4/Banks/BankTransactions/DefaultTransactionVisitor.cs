using Banks.BankAccounts;
using Banks.Transactions;

namespace Banks.BankTransactions;

public class DefaultTransactionVisitor : ITransactionVisitor
{
    private readonly IEnumerable<IBankAccount> _accounts;
    public DefaultTransactionVisitor(IEnumerable<IBankAccount> accounts)
    {
        _accounts = accounts;
    }

    public BankTransaction? BankTransaction { get; private set; }
    public void Visit(DepositTransaction transaction)
    {
        IBankAccount account = _accounts.First(a => a.Id == transaction.AccountId);
        account.Deposit(transaction.Amount);
        BankTransaction = new BankTransaction(transaction, () => account.Withdraw(transaction.Amount));
    }

    public void Visit(WithdrawTransaction transaction)
    {
        IBankAccount account = _accounts.First(a => a.Id == transaction.AccountId);
        account.Withdraw(transaction.Amount);
        BankTransaction = new BankTransaction(transaction, () => account.Deposit(transaction.Amount));
    }

    public void Visit(TransferTransaction transaction)
    {
        if (transaction.WithdrawTransaction.AccountId == transaction.DepositTransaction.AccountId)
            throw new InvalidOperationException("Cannot transfer to the same account");
        IBankAccount fromAccount = _accounts.First(a => a.Id == transaction.WithdrawTransaction.AccountId);
        IBankAccount toAccount = _accounts.First(a => a.Id == transaction.DepositTransaction.AccountId);
        fromAccount.Withdraw(transaction.Amount);
        toAccount.Deposit(transaction.Amount);
        BankTransaction = new BankTransaction(transaction, () =>
        {
            fromAccount.Deposit(transaction.Amount);
            toAccount.Withdraw(transaction.Amount);
        });
    }
}
