namespace Banks.DateObservers;

public class DefaultDateSubject : IDateSubject
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

    public void Notify()
    {
        foreach (IDateObserver observer in _observers)
        {
            observer.Update(this);
        }
    }
}
