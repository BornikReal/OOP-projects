namespace Backups;

public class Storage
{
    private readonly List<BackupObject> _backupObjects;

    public Storage(List<BackupObject> backupObjects, string stotagePath)
    {
        _backupObjects = backupObjects;
        StotagePath = stotagePath;
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public string StotagePath { get; }
    public Guid Id { get; } = Guid.NewGuid();
}
