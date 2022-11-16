using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IAlgorithm
{
    public IStorage CreateBackup(IReadOnlyCollection<BackupObject> backupObjects, string restorPointPath, IRepository repository, IArchivator archiver)
    {
        IEnumerable<IFileSystemEntity> entities = backupObjects.Select(backupObject => repository.OpenEntity(backupObject.ObjectPath));
        return archiver.CreateArchive(entities, restorPointPath, repository);
    }
}
