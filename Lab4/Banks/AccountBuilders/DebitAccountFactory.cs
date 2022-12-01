using Banks.BankAccounts;
using Banks.Models;

namespace Banks.AccountBuilders;

public class DebitAccountFactory : IBankAccountFactory
{
    public IBankAccount CreateAccount(Bank bank)
    {
        return new DebitAccount(bank.DebitInterestRate, bank.TransferLimit);
    }
}
