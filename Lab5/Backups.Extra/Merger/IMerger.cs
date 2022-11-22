using Backups.Algorithms;
using Backups.Extra.RepositorySuper;
using Backups.Models;

namespace Backups.Extra.Merger;

public interface IMerger
{
    RestorePoint Merge(IEnumerable<RestorePoint> points, IAlgorithm algorithm, IRepositorySuper repository, string restorePointPath);
}
