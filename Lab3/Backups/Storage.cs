namespace Backups;

public class Storage
{
    public Storage(BackupObject backupObject)
    {
        BackupObject = backupObject;
    }

    public BackupObject BackupObject { get; }
}
