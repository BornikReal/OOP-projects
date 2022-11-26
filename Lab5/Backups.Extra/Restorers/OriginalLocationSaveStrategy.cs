using Backups.Extra.Exceptions;
using Backups.Extra.LoggingEntities;
using Backups.Extra.RepositorySuper;
using Backups.Extra.Visitors;
using Backups.FileSystemEntities.Interfaces;
using Backups.Interlayer;
using Backups.Models;

namespace Backups.Extra.Restorers;

public class OriginalLocationSaveStrategy : IRestorer
{
    private readonly IRepositorySuper _repository;
    private readonly RestorePointVisitor _restorer;
    private readonly ILogger _logger;

    public OriginalLocationSaveStrategy(IRepositorySuper repository, ILogger logger)
    {
        _repository = repository;
        _restorer = new RestorePointVisitor();
        _logger = logger;
    }

    public void RestoreBackup(RestorePoint restorePoint)
    {
        _logger.Log($"Start restoring backup with Original location strategy on path {restorePoint.RestorePointPath}");
        IRepoDisposable interlayer = restorePoint.Storage.GetEntities();
        foreach (IFileSystemEntity entity in interlayer.Entities)
        {
            var backupObjects = restorePoint.BackupObjects.ToList();
            BackupObject backupObject = backupObjects.Find(s => s.ObjectPath[(s.ObjectPath.LastIndexOf(_repository.PathSeparator) + 1) ..] == entity.Name) !;
            _restorer.SavingPath = $"{backupObject.ObjectPath}";
            if (backupObject.Repository as IRepositorySuper != null)
                _restorer.Repository = (IRepositorySuper)backupObject.Repository;
            else
                throw new InvalidBackupObjectRepositoryException();
            entity.Accept(_restorer);
            _logger.Log($"Restored {entity.Name}");
        }

        interlayer.Dispose();
        _logger.Log("Finish restoring");
    }
}
