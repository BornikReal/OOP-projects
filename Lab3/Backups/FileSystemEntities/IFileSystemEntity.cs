using Backups.Visitors;

namespace Backups.FileSystemEntities;

public interface IFileSystemEntity
{
    string Name { get; }

    void Accept(IVisitor visitor);
}
