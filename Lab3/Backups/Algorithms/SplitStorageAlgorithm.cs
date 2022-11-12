using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm : IAlgorithm
{
    public IStorage CreateBackup(List<BackupObject> backupObjects, string restorPointPath, IRepository repository, IArchivator archiver)
    {
        var storages = new List<IStorage>();
        foreach (BackupObject backupObject in backupObjects)
        {
            var entities = new List<IFileSystemEntity>
            {
                repository.OpenEntity(backupObject.ObjectPath),
            };

            storages.Add(archiver.CreateArchive(entities, restorPointPath, repository));
        }

        return new SplitStorage(storages);
    }
}
