using Backups.Extra.Exceptions;
using Backups.Extra.LoggingEntities;
using Backups.Extra.RepositorySuper;
using Backups.Models;

namespace Backups.Extra.Deleter;

public class RestorePointDeleter : IDeleter
{
    private readonly IRepositorySuper _repository;
    private readonly ILogger _logger;
    public RestorePointDeleter(IRepositorySuper repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public void DeleteRestorePoint(IEnumerable<RestorePoint> restorePoints, IBackup backup)
    {
        if (restorePoints.Count() == backup.RestorePoints.Count())
            throw new DeletingAllRestorPointsException();
        foreach (RestorePoint point in restorePoints)
        {
            _repository.DeleteEntity(point.RestorePointPath);
            backup.RemoveRestorePoint(point);
            _logger.Log($"Restore point {point.RestorePointPath} was deleted");
        }
    }
}
