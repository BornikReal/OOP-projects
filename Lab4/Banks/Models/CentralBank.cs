using Banks.BankAccounts;
using Banks.DateObservers;
using Banks.InterestRateStrategy;

namespace Banks.Models;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks = new List<Bank>();
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();

    public Bank CreateBank(IClock dateSubject, decimal debitInterestRate, IInterestRateStrategy strategy, TimeSpan depositSpan, decimal commisionRate, decimal creditLimit)
    {
        var bank = new Bank(dateSubject, debitInterestRate, strategy, depositSpan, commisionRate, creditLimit);
        bank.Notify += OnBankAccountCreated;
        _banks.Add(bank);
        return bank;
    }

    private void OnBankAccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
