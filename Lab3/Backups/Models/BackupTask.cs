using Backups.Algorithms;
using Backups.Archiver;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Models;

public class BackupTask
{
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();

    public BackupTask(string backupTaskPath, IRepository repository, IAlgorithm algorithm, IArchivator archivator)
    {
        Repository = repository;
        Algorithm = algorithm;
        Archivator = archivator;
        BackupTaskPath = backupTaskPath;
    }

    public Backup Backup { get; } = new Backup();
    public string BackupTaskPath { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IRepository Repository { get; }
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

    public string Start()
    {
        string restorePointPath = BackupTaskPath + Repository.PathSeparator + "RestorePoint-" + Guid.NewGuid();
        IStorage storage = Algorithm.CreateBackup(_backupObjects, restorePointPath, Repository, Archivator);
        Backup.AddRestorePoint(new RestorePoint(_backupObjects, storage, restorePointPath, DateTime.Now));
        return restorePointPath;
    }
}
