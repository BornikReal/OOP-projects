using Backups.Models;

namespace Backups.Extra.Cleaner;

public class DateCleaner : ICleaner
{
    private readonly TimeSpan _span;
    public DateCleaner(TimeSpan time)
    {
        _span = time;
    }

    public IEnumerable<RestorePoint> Clean(IEnumerable<RestorePoint> restorePoints)
    {
        return restorePoints.Where(point => (DateTime.Now - point.CreationTime) > _span);
    }

    public override string ToString()
    {
        return "Date Cleaner";
    }
}
