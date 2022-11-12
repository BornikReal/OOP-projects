using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IAlgorithm
{
    public IStorage CreateBackup(List<BackupObject> backupObjects, string restorPointPath, IRepository repository, IArchivator archiver)
    {
        var entities = new List<IFileSystemEntity>();
        foreach (BackupObject backupObject in backupObjects)
        {
            entities.Add(repository.OpenEntity(backupObject.ObjectPath));
        }

        return archiver.CreateArchive(entities, restorPointPath, repository);
    }
}
