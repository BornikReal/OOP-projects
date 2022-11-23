using Backups.Extra.AlgorithmSuper;
using Backups.Extra.LoggingEntities;
using Backups.Extra.RepositorySuper;
using Backups.Models;

namespace Backups.Extra.Merger;

public interface IMerger
{
    RestorePoint Merge(IEnumerable<RestorePoint> points, IAlgorithmSuper algorithm, IRepositorySuper repository, string restorePointPath, ILogger logger);
}
