namespace Backups;

public class Storage
{
    public Storage(BackupObject backupObject, string stotagePath)
    {
        BackupObject = backupObject;
        StotagePath = stotagePath;
    }

    public BackupObject BackupObject { get; }
    public string StotagePath { get; }
    public Guid Id { get; } = Guid.NewGuid();
}
