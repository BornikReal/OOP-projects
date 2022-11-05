using Backups.Algorithms;
using Backups.Archivator;

namespace Backups.Models;

public class BackupTask
{
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();

    public BackupTask(string name, IAlgorithm algorithm, IArchivator archivator)
    {
        Name = name;
        Algorithm = algorithm;
        Archivator = archivator;
    }

    public string Name { get; }
    public Backup Backup { get; } = new Backup();
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public Guid Id { get; } = Guid.NewGuid();
    public IAlgorithm Algorithm { get; }
    public IArchivator Archivator { get; }

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

    public void CreateRestorePoint()
    {
        string restorPointPath = "RestorePoint-" + Guid.NewGuid();
        Backup.AddRestorePoint(new RestorePoint(new List<BackupObject>(_backupObjects), Algorithm.CreateBackup(_backupObjects, Archivator, restorPointPath), DateTime.Now));
    }
}
