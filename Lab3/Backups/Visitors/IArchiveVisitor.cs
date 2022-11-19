using Backups.FileSystemEntities.Interfaces;

namespace Backups.Visitors;

public interface IArchiveVisitor
{
    void Visit(IFileEntity fileEnity);
    void Visit(IDirectoryEntity directoryEnity);
}
