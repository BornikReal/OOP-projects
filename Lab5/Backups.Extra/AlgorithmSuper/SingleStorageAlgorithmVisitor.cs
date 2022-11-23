using Backups.Algorithms;
using Backups.Archiver;
using Backups.Extra.LoggingEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Extra.AlgorithmSuper;

public class SingleStorageAlgorithmVisitor : IAlgorithmSuper
{
    private readonly SingleStorageAlgorithm _algorithm;

    public SingleStorageAlgorithmVisitor(IArchiver archiver)
    {
        _algorithm = new SingleStorageAlgorithm(archiver);
    }

    public IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository, ILogger logger)
    {
        IStorage storage = _algorithm.CreateBackup(entities, restorPointPath, repository);
        logger.Log($"{this} created backup in {restorPointPath}");
        return storage;
    }

    public override string ToString()
    {
        return _algorithm.ToString();
    }
}
