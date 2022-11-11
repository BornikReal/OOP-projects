using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Archiver;

public interface IArchivator
{
    public string ArchiveExtension { get; }
    ZipStorage CreateArchive(List<IFileSystemEntity> entities, IRepository repository);
}
