namespace Banks.DateObservers;

public class DefaultClock : IClock
{
    private readonly Dictionary<TimeSpan, Action> _timeSpans = new Dictionary<TimeSpan, Action>();
    private DateTime _curDate;

    public void AddTime(TimeSpan timeSpan)
    {
        if (_timeSpans.Count == 0)
            return;
        DateTime newDate = _curDate + timeSpan;
        var prevDates = _timeSpans.ToDictionary(pair => pair.Key, pair => _curDate);
        TimeSpan minTimeSpan = _timeSpans.Min(x => x.Key);
        for (DateTime i = _curDate + minTimeSpan; i <= newDate; i += minTimeSpan)
        {
            foreach (KeyValuePair<TimeSpan, Action> span in _timeSpans)
            {
                if ((i - prevDates[span.Key]) >= span.Key)
                    span.Value();
                prevDates[span.Key] = i;
            }
        }

        _curDate = newDate;
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
        if (_timeSpans.ContainsKey(span) && _timeSpans[span].GetInvocationList().Contains(action))
        {
            if (_timeSpans[span].GetInvocationList().Length == 1)
                _timeSpans.Remove(span);
            else
                _timeSpans[span] = (_timeSpans[span] - action) !;
        }
    }
}
