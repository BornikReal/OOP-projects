using Backups.Extra.Cleaner;
using Backups.Extra.Deleter;
using Backups.Extra.Merger;
using Backups.Extra.SaveStrategy;
using Backups.Models;

namespace Backups.Extra.Models;

public interface IBackupTaskSuper : IBackupTask
{
    void CleanRestorePoints(ICleaner cleaner, IDeleter deleter);
    void Merge(IEnumerable<RestorePoint> points, IMerger merger);
    void RestoreBackup(RestorePoint restorePoint, ISaveStrategy saveStrategy);
}
