using Backups.Extra.Cleaner;
using Backups.Extra.Deleter;
using Backups.Extra.Merger;
using Backups.Extra.Wrappers;
using Backups.Models;

namespace Backups.Extra.Models;

public interface IBackupTaskSuper : IBackupTask
{
    void CleanRestorePoints(ICleaner cleaner, IRestorePointDeleter deleter);
    void Merge(IEnumerable<RestorePoint> points, IMerger merger);
    void RestoreBackupToOriginalLocation(RestorePoint restorePoint);
    void RestoreBackupToDifferentLocation(RestorePoint restorePoint, string savingPath, IRepositorySuper repository);
}
