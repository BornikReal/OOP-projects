using Backups.Models;

namespace Backups.Extra.Cleaner;

public interface ICleaner
{
    IEnumerable<RestorePoint> Clean(IEnumerable<RestorePoint> restorePoints);
}
