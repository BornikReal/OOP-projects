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
        {
            var newEntities = new List<IFileSystemEntity>
            {
                entity,
            };

            storages.Add(_archiver.CreateArchive(newEntities, restorPointPath, repository));
        }

        return new SplitStorage(storages);
    }
}
