using Banks.BankAccounts;
using Banks.BankBuilders;
using Banks.BankTransactions;
using Banks.DateObservers;
using Banks.Transactions;

namespace Banks.Models;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks = new List<Bank>();
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();
    private readonly List<BankTransaction> _transactions = new List<BankTransaction>();
    private readonly List<IClock> _timers = new List<IClock>();

    public Bank CreateBank(IBankBuilder builder)
    {
        Bank bank = builder.Build();
        bank.Notify += OnBankAccountCreated;
        _banks.Add(bank);
        return bank;
    }

    public void MakeTransaction(IAccountTransaction transaction)
    {
        var visitor = new DefaultTransactionVisitor(_accounts);
        transaction.Accept(visitor);
        _transactions.Add(visitor.BankTransaction!);
    }

    public void AddDays(int days)
    {
        if (days <= 0)
            throw new ArgumentException("Days must be positive");
        foreach (IClock timer in _timers)
            timer.AddTime(TimeSpan.FromDays(days));
    }

    private void OnBankAccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
