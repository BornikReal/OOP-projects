using Backups.Storages;

namespace Backups.Models;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;

    public RestorePoint(List<BackupObject> backupObjects, IStorage storages, DateTime creationTime)
    {
        _backupObjects = backupObjects;
        Storages = storages;
        CreationTime = creationTime;
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IStorage Storages { get; }
    public DateTime CreationTime { get; }
}
