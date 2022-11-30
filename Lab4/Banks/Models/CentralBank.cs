using Banks.BankAccounts;

namespace Banks.Models;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks = new List<Bank>();
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();

    public 

    private void OnBankAccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
