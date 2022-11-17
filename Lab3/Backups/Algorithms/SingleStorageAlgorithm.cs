using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;
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

    public IStorage CreateBackup(IReadOnlyCollection<BackupObject> backupObjects, string restorPointPath, IRepository repository)
    {
        IEnumerable<IFileSystemEntity> entities = backupObjects.Select(backupObject => repository.OpenEntity(backupObject.ObjectPath));
        return _archiver.CreateArchive(entities, restorPointPath, repository);
    }
}
