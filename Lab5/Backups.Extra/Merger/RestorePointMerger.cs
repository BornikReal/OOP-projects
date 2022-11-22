using Backups.Extra.AlgorithmSuper;
using Backups.Extra.Deleter;
using Backups.Extra.Visitor;
using Backups.Extra.Wrappers;
using Backups.Models;

namespace Backups.Extra.Merger;

public class RestorePointMerger : IMerger
{
    private readonly IDeleter _deleter;
    public RestorePointMerger(IDeleter deleter)
    {
        _deleter = deleter;
    }

    public RestorePoint Merge(IEnumerable<RestorePoint> points, IAlgorithmSuper algorithm, IRepositorySuper repositorySuper, string restorePointPath)
    {
        var visitor = new AlgorithmVisitor(points, repositorySuper, restorePointPath);
        algorithm.Accept(visitor);
        _deleter.DeleteRestorePoint(visitor.DeletablePoints!);
        return visitor.Result!;
    }
}
