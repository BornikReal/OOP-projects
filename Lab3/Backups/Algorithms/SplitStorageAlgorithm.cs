using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm : IAlgorithm
{
    public List<Storage> CreateBackup(List<BackupObject> backupObjects, string restorPointPath, IRepository repository, IArchivator archivator)
    {
        var storages = new List<Storage>();
        List<IFileSystemEntity> entities;
        string storageName;
        foreach (BackupObject backupObject in backupObjects)
        {
            storageName = restorPointPath + Path.DirectorySeparatorChar + "Storage-" + Guid.NewGuid() + archivator.Archiveextension;
            entities = new List<IFileSystemEntity>
            {
                repository.OpenEntity(backupObject.ObjectPath),
            };
            storages.Add(new Storage(entities, storageName));

            // archivator.CreateArchive(entities, repository.OpenEntity(storageName).Stream!);
            // repository.CloseEntity(entities[0]);
        }

        return storages;
    }
}
