using Backups.Algorithms;
using Backups.Archiver;
using Backups.Repository;

namespace Backups.Models;

public interface IBackupTask
{
    public Backup Backup { get; }
    public string BackupTaskPath { get; }
    public IReadOnlyList<BackupObject> BackupObjects { get; }
    public IRepository Repository { get; }
    public IAlgorithm Algorithm { get; }
    public IArchivator Archivator { get; }

    void AddNewTask(BackupObject backupObject);
    void RemoveTask(BackupObject backupObject);
    RestorePoint Start();
}
