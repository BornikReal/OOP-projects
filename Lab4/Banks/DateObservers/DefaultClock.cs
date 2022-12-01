using Banks.BankAccounts;

namespace Banks.DateObservers;

public class DefaultClock : IClock
{
    public delegate void AccountHandler(IBankAccount account);
    public event AccountHandler? Notify;
    public DateTime CurDate => DateTime.Now;

    public void NotifyNewDay()
    {
        foreach (IDateObserver observer in _observers)
        {
            observer.UpdateNewDay();
        }
    }

    public void NotifyNewMonth()
    {
        {
            foreach (IDateObserver observer in _observers)
            {
                observer.UpdateNewMonth();
            }
        }
    }
}
