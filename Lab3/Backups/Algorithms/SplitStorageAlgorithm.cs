using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm<TArchiver> : IAlgorithm
    where TArchiver : IArchiver
{
    private readonly TArchiver _archiver;
    public SplitStorageAlgorithm(TArchiver archiver)
    {
        _archiver = archiver;
    }

    public IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository)
    {
        var storages = new List<IStorage>();
        foreach (IFileSystemEntity entity in entities)
            storages.Add(_archiver.CreateArchive(new List<IFileSystemEntity> { entity }, restorPointPath, repository));

        return new SplitStorage(storages);
    }
}
