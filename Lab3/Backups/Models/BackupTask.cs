using Backups.Algorithms;
using Backups.Exceptions;
using Backups.Repository;
using Backups.Storages;
using Backups.Strategy;

namespace Backups.Models;

public class BackupTask : IBackupTask
{
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();
    private readonly IBackup _backup;

    public BackupTask(ITimeStrategy strategy, IRepository repository, IAlgorithm algorithm, IBackup backup)
    {
        Repository = repository;
        Algorithm = algorithm;
        BackupTaskPath = $"BackupTask-{Guid.NewGuid()}";
        _backup = backup;
        Repository.CreateDirectory(BackupTaskPath);
        TimeStrategy = strategy;
    }

    public ITimeStrategy TimeStrategy { get; set; }

    public string BackupTaskPath { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IRepository Repository { get; }
    public IAlgorithm Algorithm { get; }

    public void AddNewTask(BackupObject backupObject)
    {
        if (_backupObjects.Find(s => s == backupObject) != null)
            throw new BackupTaskModificationException();
        _backupObjects.Add(backupObject);
    }

    public void RemoveTask(BackupObject backupObject)
    {
        if (_backupObjects.Find(s => s == backupObject) == null)
            throw new BackupTaskModificationException();
        _backupObjects.Remove(backupObject);
    }

    public RestorePoint Start()
    {
        string restorePointPath = $"{BackupTaskPath}{Repository.PathSeparator}RestorePoint-{Guid.NewGuid()}";
        Repository.CreateDirectory(restorePointPath);
        IStorage storage = Algorithm.CreateBackup(_backupObjects.Select(s => Repository.OpenEntity(s.ObjectPath)), restorePointPath, Repository);
        var restorePoint = new RestorePoint(_backupObjects, storage, restorePointPath, TimeStrategy.SetTime());
        _backup.AddRestorePoint(restorePoint);
        return restorePoint;
    }
}
