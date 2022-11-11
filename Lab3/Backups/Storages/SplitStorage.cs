using Backups.FileSystemEntities.Interfaces;

namespace Backups.Storages;

public class SplitStorage
{
    public SplitStorage(IEnumerable<ZipStorage> storages)
    {
        Storages = storages;
    }

    public IEnumerable<ZipStorage> Storages { get; }

    public IEnumerable<IFileSystemEntity> GetEntities()
    {
        var entities = new List<IFileSystemEntity>();
        foreach (ZipStorage zipStorage in Storages)
            entities.AddRange(zipStorage.GetEntities());
        return entities;
    }
}
