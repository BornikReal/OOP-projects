using Backups.Algorithms;
using Backups.Archiver;
using Backups.Exceptions;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Models;

public class BackupTask : IBackupTask
{
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();

    public BackupTask(string backupTaskPath, IRepository repository, IAlgorithm algorithm, IArchivator archivator, IBackup? backup = null)
    {
        Repository = repository;
        Algorithm = algorithm;
        Archivator = archivator;
        BackupTaskPath = backupTaskPath;
        if (backup == null)
            Backup = new Backup();
        else
            Backup = backup;
    }

    public IBackup Backup { get; }
    public string BackupTaskPath { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IRepository Repository { get; }
    public IAlgorithm Algorithm { get; }
    public IArchivator Archivator { get; }

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
        string restorePointPath = Repository.CreateRestorePointDirectory(this);
        IStorage storage = Algorithm.CreateBackup(_backupObjects, restorePointPath, Repository, Archivator);
        var restorePoint = new RestorePoint(_backupObjects, storage, restorePointPath, DateTime.Now);
        Backup.AddRestorePoint(restorePoint);
        return restorePoint;
    }
}
