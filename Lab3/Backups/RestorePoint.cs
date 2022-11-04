namespace Backups;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;

    public RestorePoint(List<BackupObject> backupObjects, Storage storage, DateTime creationTime)
    {
        _backupObjects = backupObjects;
        Storage = storage;
        CreationTime = creationTime;
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public Storage Storage { get; }
    public DateTime CreationTime { get; }
    public Guid Id { get; } = Guid.NewGuid();
}
