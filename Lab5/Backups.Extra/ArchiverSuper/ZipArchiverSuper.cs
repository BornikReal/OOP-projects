using Backups.Archiver;
using Backups.Extra.LoggingEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Extra.ArchiverSuper;

public class ZipArchiverSuper : IArchiver
{
    private readonly ZipArchiver _archiver;
    public ZipArchiverSuper(ZipArchiver archiver, ILogger logger)
    {
        _archiver = archiver;
        Logger = logger;
    }

    public ILogger Logger { get; set; }
    public IStorage CreateArchive(IEnumerable<IFileSystemEntity> entities, string archivePath, IRepository repository)
    {
        IStorage storage = _archiver.CreateArchive(entities, archivePath, repository);
        Logger.Log($"{this} created archive in {archivePath}");
        return storage;
    }
}
