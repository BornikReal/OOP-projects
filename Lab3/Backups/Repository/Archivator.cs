using Backups.FileSystemEntities;

namespace Backups.Repository;

public class Archivator : IArchivator
{
    public void CreateArchive(List<IFileSystemEntity> entities, FileSystemRepository repository)
    {
        throw new NotImplementedException();
    }

    public void UnpackArchive(Storage storage, FileSystemRepository repository)
    {
        throw new NotImplementedException();
    }
}
