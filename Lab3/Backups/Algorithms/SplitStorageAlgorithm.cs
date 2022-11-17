using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm : IAlgorithm
{
    private readonly IArchivator _archiver;
    public SplitStorageAlgorithm(IArchivator archiver)
    {
        _archiver = archiver;
    }

    public IStorage CreateBackup(IReadOnlyCollection<BackupObject> backupObjects, string restorPointPath, IRepository repository)
    {
        var storages = new List<IStorage>();
        foreach (BackupObject backupObject in backupObjects)
        {
            var entities = new List<IFileSystemEntity>
            {
                repository.OpenEntity(backupObject.ObjectPath),
            };

            storages.Add(_archiver.CreateArchive(entities, restorPointPath, repository));
        }

        return new SplitStorage(storages);
    }
}
