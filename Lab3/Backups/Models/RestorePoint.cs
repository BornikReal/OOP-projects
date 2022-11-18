using Backups.Storages;

namespace Backups.Models;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;

    public RestorePoint(IEnumerable<BackupObject> backupObjects, IStorage storage, string restorePointPath, DateTime creationTime)
    {
        _backupObjects = new List<BackupObject>(backupObjects);
        Storage = storage;
        CreationTime = creationTime;
        RestorePointPath = restorePointPath;
    }

    public IEnumerable<BackupObject> BackupObjects => _backupObjects;
    public IStorage Storage { get; }
    public DateTime CreationTime { get; }
    public string RestorePointPath { get; }

    public bool Equals(RestorePoint other)
    {
        return _backupObjects.SequenceEqual(other._backupObjects) && CreationTime.Equals(other.CreationTime) && RestorePointPath == other.RestorePointPath;
    }
}
