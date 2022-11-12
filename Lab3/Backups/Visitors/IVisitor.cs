using Backups.FileSystemEntities.Interfaces;

namespace Backups.Visitors;

public interface IVisitor
{
    void Visit(IFileEntity fileEnity);
    void Visit(IDirectoryEntity directoryEnity);
}
