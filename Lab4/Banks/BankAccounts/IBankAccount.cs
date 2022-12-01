using Banks.Models;

namespace Banks.BankAccounts;

public interface IBankAccount : ICancelable
{
    decimal Balance { get; }
    decimal TransferLimit { get; }
    Guid Id { get; }
    IPerson Person { get; }
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    bool CanDeposit(decimal amount);
    bool CanWithdraw(decimal amount);
}
