using Backups.Archiver;
using Backups.Extra.LoggingEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Extra.ArchiverSuper;

public class ZipArchiverSuper : IArchiver
{
    private readonly ZipArchiver _archiver;
    private readonly ILogger _logger;
    public ZipArchiverSuper(ILogger logger)
    {
        _archiver = new ZipArchiver();
        _logger = logger;
    }

    public IStorage CreateArchive(IEnumerable<IFileSystemEntity> entities, string archivePath, IRepository repository)
    {
        IStorage storage = _archiver.CreateArchive(entities, archivePath, repository);
        _logger.Log($"{this} created archive in {archivePath}");
        return storage;
    }
}
