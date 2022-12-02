namespace Banks.DateObservers;

public class DefaultClock : IClock
{
    private DateTime _curDate;
    public Dictionary<TimeSpan, Action> TimeSpans { get; } = new Dictionary<TimeSpan, Action>();

    public void AddTime(TimeSpan timeSpan)
    {
        DateTime newDate = _curDate + timeSpan;
        DateTime prevDate = _curDate;
        TimeSpan minTimeSpan = TimeSpans.Min(x => x.Key);
        for (DateTime i = _curDate + minTimeSpan; i <= newDate; i += minTimeSpan)
        {
            foreach (KeyValuePair<TimeSpan, Action> span in TimeSpans)
            {
                if ((i - prevDate) >= span.Key)
                    span.Value();
            }
        }

        _curDate = newDate;
    }
}
