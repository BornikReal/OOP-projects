using Backups.Algorithms;
using Backups.Exceptions;
using Backups.Extra.AlgorithmSuper;
using Backups.Extra.Cleaner;
using Backups.Extra.Deleter;
using Backups.Extra.LoggingEntities;
using Backups.Extra.Wrappers;
using Backups.Models;
using Backups.Repository;
using Backups.Storages;
using Backups.Strategy;

namespace Backups.Extra.Models;

public class BackupTaskSuper : IBackupTaskSuper
{
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();
    private readonly IBackup _backup;

    public BackupTaskSuper(ITimeStrategy strategy, IRepositorySuper repository, IAlgorithmSuper algorithm, ILogger logger, IBackup? backup = null)
    {
        Repository = repository;
        Algorithm = algorithm;
        BackupTaskPath = $"BackupTask-{Guid.NewGuid()}";
        if (backup == null)
            _backup = new BackupSuper();
        else
            _backup = backup;
        Repository.CreateDirectory(BackupTaskPath);
        _logger = logger;
        TimeStrategy = strategy;
    }

    public BackupTaskSuper(BackupTaskSuper backupTask, ICleaner cleaner, IDeleter deleter)
    {
        Repository = backupTask.Repository;
        Algorithm = backupTask.Algorithm;
        BackupTaskPath = backupTask.BackupTaskPath;
        Logger = backupTask.Logger;
        TimeStrategy = backupTask.TimeStrategy;
        IEnumerable<RestorePoint> points = cleaner.Clean(backupTask.RestorePoints);
    }

    public ITimeStrategy TimeStrategy { get; set; }
    public string BackupTaskPath { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IEnumerable<RestorePoint> RestorePoints => new List<RestorePoint>(_backup.RestorePoints);
    public IRepositorySuper Repository { get; }
    public IAlgorithmSuper Algorithm { get; }
    public ILogger Logger { get; }

    public void AddNewTask(BackupObject backupObject)
    {
        if (_backupObjects.Find(s => s == backupObject) != null)
            throw new BackupTaskModificationException();
        _backupObjects.Add(backupObject);
        Logger.Log($"Added object {backupObject.ObjectPath} to task");
    }

    public void RemoveTask(BackupObject backupObject)
    {
        if (_backupObjects.Find(s => s == backupObject) == null)
            throw new BackupTaskModificationException();
        _backupObjects.Remove(backupObject);
        Logger.Log($"Removed object {backupObject.ObjectPath} to task");
    }

    public RestorePoint Start()
    {
        Logger.Log($"Start backuping with {Algorithm}");
        string restorePointPath = $"{BackupTaskPath}{Repository.PathSeparator}RestorePoint-{Guid.NewGuid()}";
        Repository.CreateDirectory(restorePointPath);
        IStorage storage = Algorithm.CreateBackup(_backupObjects.Select(s => Repository.OpenEntity(s.ObjectPath)), restorePointPath, Repository);
        var restorePoint = new RestorePoint(new List<BackupObject>(_backupObjects), storage, restorePointPath, TimeStrategy.SetTime());
        _backup.AddRestorePoint(restorePoint);
        Logger.Log($"Create restore point on path {restorePointPath}");
        return restorePoint;
    }
}
