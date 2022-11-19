namespace Backups.Models;

public interface IBackupTask
{
    void AddNewTask(BackupObject backupObject);
    void RemoveTask(BackupObject backupObject);
    RestorePoint Start();
}
