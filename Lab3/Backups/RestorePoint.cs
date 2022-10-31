namespace Backups;

public class RestorePoint
{
    private List<BackupObject> _backupObjects;

    public RestorePoint(List<BackupObject> backupObjects, DateTime creationTime)
    {
        _backupObjects = backupObjects;
        CreationTime = creationTime;
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public DateTime CreationTime { get; }
}
