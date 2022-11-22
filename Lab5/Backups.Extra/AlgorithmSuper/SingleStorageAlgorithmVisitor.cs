using Backups.Algorithms;
using Backups.Archiver;
using Backups.Extra.LoggingEntities;
using Backups.Extra.Visitors;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Extra.AlgorithmSuper;

public class SingleStorageAlgorithmVisitor : IAlgorithmSuper
{
    private readonly SingleStorageAlgorithm _algorithm;

    public SingleStorageAlgorithmVisitor(IArchiver archiver, ILogger logger)
    {
        _algorithm = new SingleStorageAlgorithm(archiver);
        Logger = logger;
    }

    public ILogger Logger { get; set; }

    public void Accept(IAlgorithmVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository)
    {
        IStorage storage = _algorithm.CreateBackup(entities, restorPointPath, repository);
        Logger.Log($"{this} created backup in {restorPointPath}");
        return storage;
    }

    public override string ToString()
    {
        return _algorithm.ToString();
    }
}
