namespace Banks.DateObservers;

public interface IDateSubject
{
    public DateTime CurDate { get; set; }
    void Attach(IDateObserver observer);
    void Detach(IDateObserver observer);
    void Notify();
}
