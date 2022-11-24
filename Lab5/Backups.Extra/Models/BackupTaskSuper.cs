using Backups.Algorithms;
using Backups.Exceptions;
using Backups.Extra.Cleaner;
using Backups.Extra.Deleter;
using Backups.Extra.LoggingEntities;
using Backups.Extra.RepositorySuper;
using Backups.Models;
using Backups.Storages;
using Backups.Strategy;

namespace Backups.Extra.Models;

public class BackupTaskSuper : IBackupTaskSuper
{
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();
    private readonly ILogger _logger;
    private readonly IBackupSuper _backup;

    public BackupTaskSuper(ITimeStrategy strategy, IRepositorySuper repository, IAlgorithm algorithm, ILogger logger, IBackupSuper backup)
    {
        _logger = logger;
        _logger.Log("Initialization of BackupTask");
        Repository = repository;
        Algorithm = algorithm;
        BackupTaskPath = $"BackupTask-{Guid.NewGuid()}";
        _backup = backup;
        Repository.CreateDirectory(BackupTaskPath);
        TimeStrategy = strategy;
        _logger.Log("Initialization finished");
    }

    public ITimeStrategy TimeStrategy { get; set; }
    public string BackupTaskPath { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IEnumerable<RestorePoint> RestorePoints => new List<RestorePoint>(_backup.RestorePoints);
    public IRepositorySuper Repository { get; }
    public IAlgorithm Algorithm { get; }

    public void AddNewTask(BackupObject backupObject)
    {
        if (_backupObjects.Find(s => s == backupObject) != null)
            throw new BackupTaskModificationException();
        _backupObjects.Add(backupObject);
        _logger.Log($"Added object {backupObject.ObjectPath} to task");
    }

    public void RemoveTask(BackupObject backupObject)
    {
        if (_backupObjects.Find(s => s == backupObject) == null)
            throw new BackupTaskModificationException();
        _backupObjects.Remove(backupObject);
        _logger.Log($"Removed object {backupObject.ObjectPath} to task");
    }

    public RestorePoint Start()
    {
        _logger.Log($"Start backuping with {Algorithm.GetType()}");
        string restorePointPath = $"{BackupTaskPath}{Repository.PathSeparator}RestorePoint-{Guid.NewGuid()}";
        Repository.CreateDirectory(restorePointPath);
        IStorage storage = Algorithm.CreateBackup(_backupObjects.Select(s => Repository.OpenEntity(s.ObjectPath)), restorePointPath, Repository);
        var restorePoint = new RestorePoint(new List<BackupObject>(_backupObjects), storage, restorePointPath, TimeStrategy.SetTime());
        _backup.AddRestorePoint(restorePoint);
        _logger.Log(Algorithm.ToString() !);
        _logger.Log($"Create restore point on path {restorePointPath}");
        return restorePoint;
    }

    public void CleanRestorePoints(ICleaner cleaner, IDeleter deleter)
    {
        _logger.Log("Start cleaning restor points");
        _logger.Log($"Preparing list of restor points with {cleaner}");
        IEnumerable<RestorePoint> points = cleaner.Clean(_backup.RestorePoints);
        if (points.Count() == _backup.RestorePoints.Count() && points.Any())
            throw new Exception();
        foreach (RestorePoint point in points)
            _backup.RemoveRestorePoint(point);
        deleter.DeleteRestorePoint(points);
        _logger.Log(deleter.ToString() !);
        _logger.Log("Cleaning restor points finished");
    }
}
