namespace Banks.DateObservers;

public interface IClock
{
    Dictionary<TimeSpan, Action> TimeSpans { get; }
    void AddTime(TimeSpan timeSpan);
}
