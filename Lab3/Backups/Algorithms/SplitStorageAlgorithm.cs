using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm : IAlgorithm
{
    private readonly IArchiver _archiver;
    public SplitStorageAlgorithm(IArchiver archiver)
    {
        _archiver = archiver;
    }

    public IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository)
    {
        var storages = entities.Select(entity => _archiver.CreateArchive(new List<IFileSystemEntity> { entity }, restorPointPath, repository)).ToList();
        return new SplitStorage(storages);
    }

    public override string ToString()
    {
        return "Split Storage Algorithm";
    }
}
