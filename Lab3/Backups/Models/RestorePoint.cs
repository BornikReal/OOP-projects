using Backups.Storages;

namespace Backups.Models;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;

    public RestorePoint(List<BackupObject> backupObjects, IStorage storages, string restorePointPath, DateTime creationTime)
    {
        _backupObjects = backupObjects;
        Storages = storages;
        CreationTime = creationTime;
        RestorePointPath = restorePointPath;
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IStorage Storages { get; }
    public DateTime CreationTime { get; }
    public string RestorePointPath { get; }

    public bool Equals(RestorePoint other)
    {
        return _backupObjects.SequenceEqual(other._backupObjects) && CreationTime.Equals(other.CreationTime) && RestorePointPath == other.RestorePointPath;
    }
}
