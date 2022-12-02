namespace Banks.DateObservers;

public class MonthClock : IClock
{
    private DateTime _curDate;
    public event Action? Notify;

    public void AddTime(TimeSpan timeSpan)
    {
        DateTime newDate = _curDate + timeSpan;
        if (newDate.Month != _curDate.Month)
            Notify?.Invoke();
        _curDate = newDate;
    }
}
