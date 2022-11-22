using Backups.Extra.RepositorySuper;
using Backups.Models;

namespace Backups.Extra.Deleter;

public class RestorePointDeleter : IDeleter
{
    private readonly IRepositorySuper _repository;
    private string _loggerString = string.Empty;
    public RestorePointDeleter(IRepositorySuper repository)
    {
        _repository = repository;
    }

    public void DeleteRestorePoint(IEnumerable<RestorePoint> restorePoints)
    {
        _loggerString = string.Empty;
        foreach (RestorePoint point in restorePoints)
        {
            if (_loggerString != string.Empty)
                _loggerString += "\n";
            _loggerString += $"Restore point {point.RestorePointPath} was deleted";
            _repository.DeleteEntity(point.RestorePointPath);
        }
    }

    public override string ToString()
    {
        return _loggerString;
    }
}
