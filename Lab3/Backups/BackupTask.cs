namespace Backups;

public class BackupTask
{
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();

    public BackupTask(string name, IRepository repository)
    {
        Name = name;
        Repository = repository;
    }

    public string Name { get; }
    public IRepository Repository { get; }
    public Backup Backup { get; } = new Backup();
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;

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
