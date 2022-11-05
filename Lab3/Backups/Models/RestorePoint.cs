using Backups.Storages;

namespace Backups.Models;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;
    private readonly List<Storage> _storages;

    public RestorePoint(List<BackupObject> backupObjects, List<Storage> storages, DateTime creationTime)
    {
        _backupObjects = backupObjects;
        _storages = storages;
        CreationTime = creationTime;
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IReadOnlyList<Storage> Storages => _storages;
    public DateTime CreationTime { get; }
    public Guid Id { get; } = Guid.NewGuid();
}
