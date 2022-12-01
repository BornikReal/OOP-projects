namespace Banks.DateObservers;

public interface IClock
{
    public DateTime CurDate { get; }
    void NotifyNewDay();
    void NotifyNewMonth();
}
