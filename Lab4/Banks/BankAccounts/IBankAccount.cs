using Banks.Models;

namespace Banks.BankAccounts;

public interface IBankAccount
{
    decimal Balance { get; }
    decimal TransferLimit { get; }
    Guid Id { get; }
    IPerson Person { get; }
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
}
