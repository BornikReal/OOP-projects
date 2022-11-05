using Backups.FileSystemEntities;

namespace Backups.Archivator;

public interface IArchivator
{
    public string Archiveextension { get; }
    void CreateArchive(List<IFileSystemEntity> entities, Stream archiveStream);
}
