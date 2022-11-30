using Banks.BankAccounts;

namespace Banks.Models;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks = new List<Bank>();
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();

    public Bank CreateBank()
    {
        throw new NotImplementedException();
    }

    private void OnBankAccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
