using Backups.FileSystemEntities;

namespace Backups;

public class Storage
{
    private readonly List<IFileSystemEntity> _entities;

    public Storage(List<IFileSystemEntity> entities, string stotagePath)
    {
        _entities = entities;
        StotagePath = stotagePath;
    }

    public IReadOnlyList<IFileSystemEntity> Entities => _entities;
    public string StotagePath { get; }
    public Guid Id { get; } = Guid.NewGuid();
    public IFileSystemEntity? FindEntity(string name)
    {
        return _entities.Find(s => s.Name == name);
    }
}
