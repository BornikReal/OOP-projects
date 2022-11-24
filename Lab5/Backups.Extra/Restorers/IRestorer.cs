using Backups.Models;

namespace Backups.Extra.Restorers;

public interface IRestorer
{
    void RestoreBackup(RestorePoint restorePoint);
}
