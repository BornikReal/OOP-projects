using Banks.BankAccounts;
using Banks.BankBuilders;
using Banks.BankTransactions;
using Banks.Transactions;

namespace Banks.Models;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks = new List<Bank>();
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();
    private readonly List<BankTransaction> _transactions = new List<BankTransaction>();

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

    private void OnBankAccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
