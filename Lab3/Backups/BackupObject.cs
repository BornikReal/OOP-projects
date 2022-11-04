namespace Backups;

public class BackupObject
{
    public BackupObject(string objectPath)
    {
        ObjectPath = objectPath;
    }

    public string ObjectPath { get; }
}
