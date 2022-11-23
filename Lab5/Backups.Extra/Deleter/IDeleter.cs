using Backups.Extra.LoggingEntities;
using Backups.Models;

namespace Backups.Extra.Deleter;

public interface IDeleter
{
    void DeleteRestorePoint(IEnumerable<RestorePoint> restorePoints, ILogger logger);
}
