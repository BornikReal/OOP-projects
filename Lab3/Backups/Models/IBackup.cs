namespace Backups.Models;

public interface IBackup
{
    public IEnumerable<RestorePoint> RestorePoints { get; }
    void AddRestorePoint(RestorePoint restorePoint);
    void RemoveRestorePoint(RestorePoint restorePoint);
}
