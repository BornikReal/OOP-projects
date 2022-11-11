using Backups.FileSystemEntities.Interfaces;

namespace Backups.Storages;

public class SplitStorage
{
    public SplitStorage(IEnumerable<IStorage> storages)
    {
        Storages = storages;
    }

    public IEnumerable<IStorage> Storages { get; }

    public IEnumerable<IFileSystemEntity> GetEntities()
    {
        var entities = new List<IFileSystemEntity>();
        foreach (IStorage storage in Storages)
            entities.AddRange(storage.GetEntities());
        return entities;
    }
}
