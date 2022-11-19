using Backups.Models;

namespace Backups.Extra.Deleter;

public interface IRestorePointDeleter
{
    void DeleteRestorePoint(IEnumerable<RestorePoint> restorePoints);
}
