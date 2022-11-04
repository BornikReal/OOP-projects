namespace Backups;

public class BackupTask
{
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();

    public BackupTask(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public Backup Backup { get; } = new Backup();
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public Guid Id { get; } = Guid.NewGuid();

    public void AddNewTask(BackupObject backupObject)
    {
        if (_backupObjects.Find(s => s == backupObject) != null)
            throw new Exception();
        _backupObjects.Add(backupObject);
    }

    public void RemoveTask(BackupObject backupObject)
    {
        if (_backupObjects.Find(s => s == backupObject) == null)
            throw new Exception();
        _backupObjects.Remove(backupObject);
    }
}
