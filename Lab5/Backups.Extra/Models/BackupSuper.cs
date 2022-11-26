using Backups.Extra.Cleaner;
using Backups.Extra.Deleter;
using Backups.Models;

namespace Backups.Extra.Models;

public class BackupSuper : IBackup, ICleanable
{
    private readonly IDeleter _deleter;
    private readonly ICleaner _cleaner;
    private readonly Backup _backup;

    public BackupSuper(IDeleter deleter, ICleaner cleaner, string backupPath)
    {
        _deleter = deleter;
        _cleaner = cleaner;
        BackupPath = backupPath;
        _backup = new Backup();
    }

    public IEnumerable<RestorePoint> RestorePoints => _backup.RestorePoints;
    public string BackupPath { get; }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        _backup.AddRestorePoint(restorePoint);
        Clean(_cleaner, _deleter);
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        _backup.RemoveRestorePoint(restorePoint);
    }

    public void Clean(ICleaner cleaner, IDeleter deleter)
    {
        deleter.DeleteRestorePoint(cleaner.Clean(RestorePoints), _backup);
    }
}
