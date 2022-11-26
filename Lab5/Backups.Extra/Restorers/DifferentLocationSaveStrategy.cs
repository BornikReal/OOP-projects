using Backups.Extra.LoggingEntities;
using Backups.Extra.RepositorySuper;
using Backups.Extra.Visitors;
using Backups.FileSystemEntities.Interfaces;
using Backups.Interlayer;
using Backups.Models;

namespace Backups.Extra.Restorers;

public class DifferentLocationSaveStrategy : IRestorer
{
    private readonly string _savingPath;
    private readonly IRepositorySuper _repository;
    private readonly RestorePointVisitor _restorer;
    private readonly ILogger _logger;

    public DifferentLocationSaveStrategy(string savingPath, IRepositorySuper repository, ILogger logger)
    {
        _savingPath = savingPath;
        _repository = repository;
        _restorer = new RestorePointVisitor();
        _logger = logger;
    }

    public void RestoreBackup(RestorePoint restorePoint)
    {
        _logger.Log($"Start restoring backup with Different location strategy on path {restorePoint.RestorePointPath}");
        IRepoDisposable interlayer = restorePoint.Storage.GetEntities();
        _restorer.Repository = _repository;
        foreach (IFileSystemEntity entity in interlayer.Entities)
        {
            var backupObjects = restorePoint.BackupObjects.ToList();
            BackupObject backupObject = backupObjects.Find(s => s.ObjectPath[(s.ObjectPath.LastIndexOf(_repository.PathSeparator) + 1) ..] == entity.Name) !;
            _restorer.SavingPath = $"{_savingPath}{_repository.PathSeparator}{backupObject.ObjectPath[(backupObject.ObjectPath.LastIndexOf(_repository.PathSeparator) + 1) ..]}";
            entity.Accept(_restorer);
            _logger.Log($"Restored {entity.Name}");
        }

        interlayer.Dispose();
    }
}
