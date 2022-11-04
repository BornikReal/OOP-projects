using Backups.FileSystemEntities;

namespace Backups.Repository;

public interface IArchivator
{
    public FileSystemRepository Repository { get; }
    public string Archiveextension { get; }
    void CreateArchive(List<IFileSystemEntity> entities, string fullArchiveName);
    void UnpackArchive(Storage storage, string fullPathUnpackDirectory);
}
