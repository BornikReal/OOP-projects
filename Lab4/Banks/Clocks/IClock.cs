namespace Banks.DateObservers;

public interface IClock
{
    void Subscribe(TimeSpan span, Action action);
    void Unsubscribe(TimeSpan span, Action action);
    void AddTime(TimeSpan timeSpan);
}
