using Backups.Archivator;
using Backups.FileSystemEntities;
using Backups.Repository;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IAlgorithm
{
    public List<Storage> CreateBackup(List<BackupObject> backupObjects, string restorPointPath, IRepository repository, IArchivator archivator)
    {
        var entities = new List<IFileSystemEntity>();
        foreach (BackupObject backupObject in backupObjects)
        {
            entities.Add(repository.OpenEntity(backupObject.ObjectPath));
        }

        string storageName = restorPointPath + Path.DirectorySeparatorChar + "Storage-" + Guid.NewGuid() + archivator.Archiveextension;
        var storages = new List<Storage>
        {
            new Storage(entities, storageName),
        };
        archivator.CreateArchive(entities, repository.OpenEntity(storageName).Stream!);
        entities.ForEach(s => repository.CloseEntity(s));
        return storages;
    }

    public void UnpackBackup(List<Storage> storages, string unpackFolder, IRepository repository, IArchivator archivator)
    {
        if (storages.Count != 1)
            throw new Exception();
        archivator.UnpackArchive(storages[0], repository.OpenEntity());
    }
}
