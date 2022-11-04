using Backups.FileSystemEntities;

namespace Backups.Repository;

public interface IArchivator
{
    void CreateArchive(List<IFileSystemEntity> entities, FileSystemRepository repository);
    void UnpackArchive(Storage storage, FileSystemRepository repository);
}
