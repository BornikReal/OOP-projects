using Backups.Algorithms;
using Backups.Archiver;
using Backups.Extra.LoggingEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Extra.AlgorithmSuper;

public class SingleStorageAlgorithmVisitor : IAlgorithm
{
    private readonly SingleStorageAlgorithm _algorithm;
    private readonly ILogger _logger;

    public SingleStorageAlgorithmVisitor(IArchiver archiver, ILogger logger)
    {
        _algorithm = new SingleStorageAlgorithm(archiver);
        _logger = logger;
    }

    public IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository)
    {
        IStorage storage = _algorithm.CreateBackup(entities, restorPointPath, repository);
        _logger.Log($"{this} created backup in {restorPointPath}");
        return storage;
    }

    public override string ToString()
    {
        return _algorithm.ToString();
    }
}
