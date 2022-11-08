using Backups.Visitors;

namespace Backups.FileSystemEntities;
public class DirectoryEntity : IDirectoryEntity
{
    public DirectoryEntity(string name, Func<IEnumerable<IFileSystemEntity>> entities)
    {
        Name = name;
        Entities = entities;
    }

    public string Name { get; }

    public Func<IEnumerable<IFileSystemEntity>> Entities { get; }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
