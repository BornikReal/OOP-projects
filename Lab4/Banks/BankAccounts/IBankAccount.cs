using Banks.Models;
using Banks.Notificators;

namespace Banks.BankAccounts;

public interface IBankAccount : ICancelable
{
    decimal Balance { get; }
    decimal TransferLimit { get; }
    Guid Id { get; }
    IPerson Person { get; }
    INotificatorStrategy? ClienNotificator { get; set; }
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    bool CanDeposit(decimal amount);
    bool CanWithdraw(decimal amount);
    void Update(Bank bank);
}
