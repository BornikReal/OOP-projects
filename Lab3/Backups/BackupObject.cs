namespace Backups;

public class BackupObject
{
    public BackupObject(string objectPath)
    {
        ObjectPath = objectPath;
    }

    public string ObjectPath { get; }
    public Guid Id { get; } = Guid.NewGuid();
}
