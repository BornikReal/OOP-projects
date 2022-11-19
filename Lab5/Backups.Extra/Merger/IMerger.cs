using Backups.Extra.AlgorithmSuper;
using Backups.Extra.Wrappers;
using Backups.Models;

namespace Backups.Extra.Merger;

public interface IMerger
{
    RestorePoint Merge(IEnumerable<RestorePoint> points, IAlgorithmSuper algorithm, IRepositorySuper repositorySuper, string restorePointPath);
}
