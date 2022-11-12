using Backups.Storages;

namespace Backups.Models;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;

    public RestorePoint(List<BackupObject> backupObjects, IStorage storage, string restorePointPath, DateTime creationTime)
    {
        _backupObjects = backupObjects;
        Storage = storage;
        CreationTime = creationTime;
        RestorePointPath = restorePointPath;
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IStorage Storage { get; }
    public DateTime CreationTime { get; }
    public string RestorePointPath { get; }

    public bool Equals(RestorePoint other)
    {
        return _backupObjects.SequenceEqual(other._backupObjects) && CreationTime.Equals(other.CreationTime) && RestorePointPath == other.RestorePointPath;
    }
}
