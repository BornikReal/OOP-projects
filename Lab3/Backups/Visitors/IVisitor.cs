using Backups.FileSystemEntities;

namespace Backups.Visitors;

public interface IVisitor
{
    void Visit(IFileEntity fileEnity);
    void Visit(IDirectoryEntity directoryEnity);
}
