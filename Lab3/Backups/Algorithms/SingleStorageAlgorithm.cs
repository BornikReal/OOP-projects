using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IAlgorithm
{
    private readonly IArchiver _archiver;
    public SingleStorageAlgorithm(IArchiver archiver)
    {
        _archiver = archiver;
    }

    public IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository)
    {
        IStorage storage = _archiver.CreateArchive(entities, restorPointPath, repository);
        return storage;
    }

    public override string ToString()
    {
        return $"Single Storage Algorithm with {_archiver}";
    }
}
