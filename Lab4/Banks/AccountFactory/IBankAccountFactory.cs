using Banks.BankAccounts;
using Banks.Models;

namespace Banks.AccountBuilders;

public interface IBankAccountFactory
{
    public IBankAccount CreateAccount(Bank bank);
}
