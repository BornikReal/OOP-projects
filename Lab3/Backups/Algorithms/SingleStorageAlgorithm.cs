using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IAlgorithm
{
    private readonly IArchiver _archiver;
    private string _loggerString = string.Empty;
    public SingleStorageAlgorithm(IArchiver archiver)
    {
        _archiver = archiver;
    }

    public IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository)
    {
        IStorage storage = _archiver.CreateArchive(entities, restorPointPath, repository);
        _loggerString = $"{_archiver}\nSaved all files in archive in directory {restorPointPath} with Single Storage Algorithm";
        return storage;
    }

    public override string ToString()
    {
        return _loggerString;
    }
}
