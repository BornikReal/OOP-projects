using Backups.Algorithms;
using Backups.Archiver;
using Backups.Extra.AlgorithmSuper;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Extra.Visitor;

public class SplitStorageAlgorithmVisitor : IAlgorithmSuper
{
    private readonly SplitStorageAlgorithm _algorithm;

    public SplitStorageAlgorithmVisitor(IArchiver archiver)
    {
        _algorithm = new SplitStorageAlgorithm(archiver);
    }

    public void Accept(IAlgorithmVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository)
    {
        return _algorithm.CreateBackup(entities, restorPointPath, repository);
    }

    public override string ToString()
    {
        return _algorithm.ToString();
    }
}
