using Backups.FileSystemEntities.Interfaces;

namespace Backups.Storages;

public class SplitStorageAdapter : IStorage
{
    private readonly SplitStorage _adaptee;

    public SplitStorageAdapter(SplitStorage adaptee)
    {
        _adaptee = adaptee;
    }

    public IEnumerable<IFileSystemEntity> GetEntities()
    {
        var entities = new List<IFileSystemEntity>();
        foreach (ZipStorage zipStorage in _adaptee.Storages)
            entities.AddRange(zipStorage.GetEntities());
        return entities;
    }
}
