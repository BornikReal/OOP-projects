using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Archiver;

public interface IArchiver
{
    IStorage CreateArchive(IEnumerable<IFileSystemEntity> entities, string archivePath, IRepository repository);
}
