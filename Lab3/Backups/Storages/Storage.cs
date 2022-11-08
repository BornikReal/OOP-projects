using Backups.FileSystemEntities.Interfaces;

namespace Backups.Storages;

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
}
