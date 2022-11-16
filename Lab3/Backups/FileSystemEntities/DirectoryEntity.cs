using Backups.FileSystemEntities.Interfaces;
using Backups.Visitors;

namespace Backups.FileSystemEntities;
public class DirectoryEntity : IDirectoryEntity
{
    private readonly Func<IEnumerable<IFileSystemEntity>> _entities;
    public DirectoryEntity(string name, Func<IEnumerable<IFileSystemEntity>> entities)
    {
        Name = name;
        _entities = entities;
    }

    public string Name { get; }

    public IEnumerable<IFileSystemEntity> Entities() => _entities();

    public void Accept(IArchiveVisitor visitor)
    {
        visitor.Visit(this);
    }
}
