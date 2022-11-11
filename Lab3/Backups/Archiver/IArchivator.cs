using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Archiver;

public interface IArchivator
{
    public string ArchiveExtension { get; }
    IStorage CreateArchive(List<IFileSystemEntity> entities, string archivePath, IRepository repository);
}
