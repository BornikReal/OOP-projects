using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm : IAlgorithm
{
    private readonly IArchiver _archiver;
    private string _loggerString = string.Empty;
    public SplitStorageAlgorithm(IArchiver archiver)
    {
        _archiver = archiver;
    }

    public IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository)
    {
        var storages = new List<IStorage>();
        _loggerString = $"{_archiver}";
        foreach (IFileSystemEntity entity in entities)
        {
            storages.Add(_archiver.CreateArchive(new List<IFileSystemEntity> { entity }, restorPointPath, repository));
            _loggerString += $"\nSaved {entity.Name} in archive in directory {restorPointPath} with Split Storage Algorithm";
        }

        return new SplitStorage(storages);
    }

    public override string ToString()
    {
        return _loggerString;
    }
}
