namespace Banks.DateObservers;

public interface IDateObserver
{
    void Update(IDateSubject dateSubject);
}
