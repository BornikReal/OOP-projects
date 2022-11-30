namespace Banks.BankAccounts;

public interface IBankAccount
{
    decimal Balance { get; }
    decimal InterestRate { get; }
    decimal ComissionRate { get; }
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
}
