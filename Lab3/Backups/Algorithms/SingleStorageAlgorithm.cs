using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IAlgorithm
{
    private readonly IArchivator _archiver;
    public SingleStorageAlgorithm(IArchivator archiver)
    {
        _archiver = archiver;
    }

    public IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository)
    {
        return _archiver.CreateArchive(entities, restorPointPath, repository);
    }
}
