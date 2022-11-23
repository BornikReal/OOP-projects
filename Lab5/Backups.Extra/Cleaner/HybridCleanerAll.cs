using Backups.Models;

namespace Backups.Extra.Cleaner;

public class HybridCleanerAll : ICleaner
{
    private readonly IEnumerable<ICleaner> _cleaners;
    public HybridCleanerAll(IEnumerable<ICleaner> cleaners)
    {
        _cleaners = cleaners;
    }

    public IEnumerable<RestorePoint> Clean(IEnumerable<RestorePoint> restorePoints)
    {
        IEnumerable<RestorePoint> result = new List<RestorePoint>();
        foreach (ICleaner cleaner in _cleaners)
            result = result.Intersect(cleaner.Clean(restorePoints));
        return result;
    }

    public override string ToString()
    {
        string result = "Hybrid Cleaner of at least one";
        foreach (ICleaner cleaner in _cleaners)
            result += $" {_cleaners}";
        return result;
    }
}
