using Backups.Models;

namespace Backups.Extra.Cleaner;

public class HybridCleanerAtLeastOne : ICleaner
{
    private readonly IEnumerable<ICleaner> _cleaners;
    public HybridCleanerAtLeastOne(IEnumerable<ICleaner> cleaners)
    {
        _cleaners = cleaners;
    }

    public IEnumerable<RestorePoint> Clean(IEnumerable<RestorePoint> restorePoints)
    {
        IEnumerable<RestorePoint> result = new List<RestorePoint>();
        foreach (ICleaner cleaner in _cleaners)
            result = result.Union(cleaner.Clean(restorePoints));
        return result;
    }
}
