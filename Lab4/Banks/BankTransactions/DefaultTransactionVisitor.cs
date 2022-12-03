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
        BankTransaction = new BankTransaction(transaction, account.Cancel());
    }

    public void Visit(WithdrawTransaction transaction)
    {
        IBankAccount account = _accounts.First(a => a.Id == transaction.AccountId);
        account.Withdraw(transaction.Amount);
        BankTransaction = new BankTransaction(transaction, account.Cancel());
    }

    public void Visit(TransferTransaction transaction)
    {
        if (transaction.FromAccountId == transaction.ToAccountId)
            throw new InvalidOperationException("Cannot transfer to the same account");
        IBankAccount fromAccount = _accounts.First(a => a.Id == transaction.FromAccountId);
        IBankAccount toAccount = _accounts.First(a => a.Id == transaction.ToAccountId);
        if (!fromAccount.CanWithdraw(transaction.Amount) || !toAccount.CanDeposit(transaction.Amount))
            throw new InvalidOperationException("Insufficient funds");
        fromAccount.Withdraw(transaction.Amount);
        toAccount.Deposit(transaction.Amount);
        BankTransaction = new BankTransaction(transaction, () =>
        {
            fromAccount.Cancel();
            toAccount.Cancel();
        });
    }
}
