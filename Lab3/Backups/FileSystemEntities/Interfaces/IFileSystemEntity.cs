using Backups.Visitors;

namespace Backups.FileSystemEntities.Interfaces;

public interface IFileSystemEntity
{
    string Name { get; }

    void Accept(IArchiveVisitor visitor);
}
