using Backups.Extra.LoggingEntities;
using Backups.Extra.RepositorySuper;
using Backups.Models;

namespace Backups.Extra.Deleter;

public class RestorePointDeleter : IDeleter
{
    private readonly IRepositorySuper _repository;
    public RestorePointDeleter(IRepositorySuper repository)
    {
        _repository = repository;
    }

    public void DeleteRestorePoint(IEnumerable<RestorePoint> restorePoints, ILogger logger)
    {
        foreach (RestorePoint point in restorePoints)
        {
            _repository.DeleteEntity(point.RestorePointPath);
            logger.Log($"Restore point {point.RestorePointPath} was deleted");
        }
    }
}
