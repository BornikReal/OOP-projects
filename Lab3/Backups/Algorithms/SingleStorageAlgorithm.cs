using Backups.Archivator;
using Backups.FileSystemEntities;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IAlgorithm
{
    public List<Storage> CreateBackup(List<BackupObject> backupObjects, IArchivator archivator, string restorPointPath)
    {
        var entities = new List<IFileSystemEntity>();
        foreach (BackupObject backupObject in backupObjects)
        {
            entities.Add(archivator.Repository.OpenEntity(backupObject.ObjectPath));
        }

        string storageName = restorPointPath + Path.DirectorySeparatorChar + "Storage-" + Guid.NewGuid() + archivator.Archiveextension;
        var storages = new List<Storage>
        {
            new Storage(entities, storageName),
        };
        archivator.CreateArchive(entities, storageName);
        entities.ForEach(s => archivator.Repository.CloseEntity(s));
        return storages;
    }

    public void UnpackBackup(IArchivator archivator, List<Storage> storages, string unpackFolder)
    {
        if (storages.Count != 1)
            throw new Exception();
        archivator.UnpackArchive(storages[0], unpackFolder);
    }
}
