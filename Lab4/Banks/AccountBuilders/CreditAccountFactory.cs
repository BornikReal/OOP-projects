using Banks.BankAccounts;
using Banks.Models;

namespace Banks.AccountBuilders;

public class CreditAccountFactory : IBankAccountFactory
{
    public IBankAccount CreateAccount(Bank bank)
    {
        return new CreditAccount(bank.ComissionRate, bank.CreditLimit, bank.TransferLimit);
    }
}
