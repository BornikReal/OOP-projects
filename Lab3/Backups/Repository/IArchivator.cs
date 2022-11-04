using Backups.FileSystemEntities;

namespace Backups.Repository;

public interface IArchivator
{
    void CreateArchive(List<IFileSystemEntity> entities, string fullArchiveName);
    void UnpackArchive(Storage storage, string fullPathUnpackDirectory);
}
