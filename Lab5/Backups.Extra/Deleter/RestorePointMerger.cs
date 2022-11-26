using Backups.Algorithms;
using Backups.Extra.Comparers;
using Backups.Extra.LoggingEntities;
using Backups.Extra.RepositorySuper;
using Backups.FileSystemEntities.Interfaces;
using Backups.Interlayer;
using Backups.Models;
using Backups.Storages;

namespace Backups.Extra.Deleter;

public class RestorePointMerger : IDeleter
{
    private readonly ILogger _logger;
    private readonly IAlgorithm _algorithm;
    private readonly IRepositorySuper _repository;
    private readonly string _backupTaskPath;

    public RestorePointMerger(IAlgorithm algorithm, IRepositorySuper repository, string backupTaskPath, ILogger logger)
    {
        _logger = logger;
        _algorithm = algorithm;
        _repository = repository;
        _backupTaskPath = backupTaskPath;
    }

    public void DeleteRestorePoint(IEnumerable<RestorePoint> restorePoints, IBackup backup)
    {
        if (!restorePoints.Any())
            return;
        RestorePoint point;
        var newPoints = restorePoints.ToList();
        var disp = new List<IRepoDisposable>();
        var comparerEntity = new FileSystemEntitiesComparer();
        var comparerBackupObject = new BackupObjectComparer();
        IEnumerable<IFileSystemEntity> enities = new List<IFileSystemEntity>();
        IEnumerable<BackupObject> backupObjects = new List<BackupObject>();
        while (newPoints.Any())
        {
            point = newPoints.MaxBy(t => t.CreationTime) !;
            disp.Add(point.Storage.GetEntities());
            enities = enities.Union(disp.Last().Entities, comparerEntity);
            backupObjects = backupObjects.Union(point.BackupObjects, comparerBackupObject);
            newPoints.Remove(point);
        }

        string restorePointPath = $"{_backupTaskPath}{_repository.PathSeparator}{Guid.NewGuid()}";
        _logger.Log("Made union of all points");
        _repository.CreateDirectory(restorePointPath);
        _logger.Log("Created directory of new restore point");
        IStorage newStorage = _algorithm.CreateBackup(enities, restorePointPath, _repository);
        _logger.Log("Archived files of new restore point");
        point = new RestorePoint(backupObjects, newStorage, restorePointPath, DateTime.Now);
        _logger.Log("Created new restore point");
        foreach (IRepoDisposable disposable in disp)
        {
            disposable.Dispose();
        }

        foreach (RestorePoint restPoint in restorePoints)
        {
            _repository.DeleteEntity(restPoint.RestorePointPath);
            backup.RemoveRestorePoint(restPoint);
            _logger.Log($"Restore point {restPoint.RestorePointPath} was deleted");
        }

        backup.AddRestorePoint(point);
    }
}
