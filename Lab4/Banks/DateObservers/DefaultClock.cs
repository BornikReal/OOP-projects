namespace Banks.DateObservers;

public class DefaultClock : IClock
{
    private readonly Dictionary<TimeSpan, Action> _timeSpans = new Dictionary<TimeSpan, Action>();
    private DateTime _curDate;

    public void AddTime(TimeSpan timeSpan)
    {
        DateTime newDate = _curDate + timeSpan;
        DateTime prevDate = _curDate;
        TimeSpan minTimeSpan = _timeSpans.Min(x => x.Key);
        for (DateTime i = _curDate + minTimeSpan; i <= newDate; i += minTimeSpan)
        {
            foreach (KeyValuePair<TimeSpan, Action> span in _timeSpans)
            {
                if ((i - prevDate) >= span.Key)
                    span.Value();
            }
        }

        _curDate = prevDate;
    }

    public void Subscribe(TimeSpan span, Action action)
    {
        if (_timeSpans.ContainsKey(span))
            _timeSpans[span] += action;
        else
            _timeSpans.Add(span, action);
    }

    public void Unsubscribe(TimeSpan span, Action action)
    {
        if (_timeSpans.ContainsKey(span))
        {
            if (_timeSpans[span].GetInvocationList().Contains(action))
                _timeSpans.Remove(span);
            else
                _timeSpans[span] = (_timeSpans[span] - action) !;
        }
    }
}
