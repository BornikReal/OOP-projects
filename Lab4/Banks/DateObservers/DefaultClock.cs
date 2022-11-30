namespace Banks.DateObservers;

public class DefaultClock : IClock
{
    private readonly List<IDateObserver> _observers = new List<IDateObserver>();
    public DateTime CurDate => DateTime.Now;
    public void Attach(IDateObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IDateObserver observer)
    {
        _observers.Remove(observer);
    }

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
