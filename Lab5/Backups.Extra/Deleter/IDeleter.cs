using Backups.Models;

namespace Backups.Extra.Deleter;

public interface IDeleter
{
    void DeleteRestorePoint(IEnumerable<RestorePoint> restorePoints);
}
