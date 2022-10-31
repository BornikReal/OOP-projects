namespace Backups;

public class BackupTask
{
    private List<RestorePoint> _restorePoints = new List<RestorePoint>();
    private List<BackupObject> _backupObjects;

    public BackupTask(List<BackupObject> backupObjects)
    {
        _backupObjects = backupObjects;
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
}
