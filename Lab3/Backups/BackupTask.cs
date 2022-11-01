namespace Backups;

public class BackupTask
{
    private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();

    public BackupTask() { }

    public BackupTask(List<BackupObject> backupObjects)
    {
        _backupObjects = backupObjects;
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;
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
