using Backups.FileSystemEntities.Interfaces;

namespace Backups.Storages;

public class SplitStorageAdapter : IStorage
{
    private readonly SplitStorage _splitStorage;

    public SplitStorageAdapter(SplitStorage splitStorage)
    {
        _splitStorage = splitStorage;
    }

    public IEnumerable<IFileSystemEntity> GetEntities()
    {
        var entities = new List<IFileSystemEntity>();
        foreach (ZipStorage zipStorage in _splitStorage.Storages)
            entities.AddRange(zipStorage.GetEntities());
        return entities;
    }
}
