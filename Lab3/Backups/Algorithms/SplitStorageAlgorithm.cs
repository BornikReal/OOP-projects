using Backups.FileSystemEntities;
using Backups.Repository;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm : IAlgorithm
{
    public List<Storage> CreateBackup(List<BackupObject> backupObjects, IArchivator archivator, string restorPointPath)
    {
        var storages = new List<Storage>();
        List<IFileSystemEntity> entities;
        string storageName;
        foreach (BackupObject backupObject in backupObjects)
        {
            storageName = restorPointPath + Path.DirectorySeparatorChar + "Storage-" + Guid.NewGuid() + archivator.Archiveextension;
            entities = new List<IFileSystemEntity>
            {
                archivator.Repository.OpenEntity(backupObject.ObjectPath),
            };
            storages.Add(new Storage(entities, storageName));
            archivator.CreateArchive(entities, storageName);
            archivator.Repository.CloseEntity(entities[0]);
        }

        return storages;
    }

    public void UnpackBackup(IArchivator archivator, List<Storage> storages, string unpackFolder)
    {
        storages.ForEach(s => archivator.UnpackArchive(s, unpackFolder));
    }
}
