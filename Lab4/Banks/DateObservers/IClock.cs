namespace Banks.DateObservers;

public interface IClock
{
    public DateTime CurDate { get; }
    void Attach(IDateObserver observer);
    void Detach(IDateObserver observer);
    void NotifyNewDay();
    void NotifyNewMonth();
}
