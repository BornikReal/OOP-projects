using Banks.BankAccounts;
using Banks.BankBuilders;

namespace Banks.Models;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks = new List<Bank>();
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();

    public Bank CreateBank(IBankBuilder builder)
    {
        Bank bank = builder.Build();
        bank.Notify += OnBankAccountCreated;
        _banks.Add(bank);
        return bank;
    }

    private void OnBankAccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
