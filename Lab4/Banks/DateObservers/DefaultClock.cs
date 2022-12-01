using Banks.BankAccounts;
using Banks.Models;

namespace Banks.DateObservers;

public class DefaultClock : IClock
{
    public delegate void AccountHandler(IBankAccount account);
    public event AccountHandler? Notify;
    public DateTime CurDate => DateTime.Now;

    public void NotifyNewDay()
    {
        Notify?.Invoke(new DebitAccount(0, 0, new Person("s", "s", null, null)));
    }

    public void NotifyNewMonth()
    {
    }
}
