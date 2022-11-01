namespace Backups;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;
    private readonly List<Storage> _storages = new List<Storage>();

    public RestorePoint(List<BackupObject> backupObjects, DateTime creationTime)
    {
        _backupObjects = backupObjects;
        CreationTime = creationTime;
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IReadOnlyList<Storage> Storages => _storages;
    public DateTime CreationTime { get; }
    public Guid Id { get; } = Guid.NewGuid();
}
