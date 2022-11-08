using Backups.FileSystemEntities.Interfaces;

namespace Backups.Archiver;

public interface IArchivator
{
    public string Archiveextension { get; }
    void CreateArchive(List<IFileSystemEntity> entities, Stream archiveStream);
}
