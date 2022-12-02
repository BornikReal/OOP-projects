using Banks.BankBuilders;
using Banks.Transactions;

namespace Banks.Models;

public interface ICentralBank
{
    Bank CreateBank(IBankBuilder builder);
    void MakeTransaction(IAccountTransaction transaction);
    void AddDays(int days);
}
