using Backups.Models;

namespace Backups.Extra.Models;

public interface IBackupSuper : IBackup
{
    void RemoveRestorePoint(RestorePoint restorePoint);
    void Clean();
}
