namespace Backups.Models;

public interface IBackup
{
    public IReadOnlyList<RestorePoint> RestorePoints { get; }
    void AddRestorePoint(RestorePoint restorePoint);
}
