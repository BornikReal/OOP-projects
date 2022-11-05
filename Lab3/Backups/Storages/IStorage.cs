using Backups.FileSystemEntities;

namespace Backups.Storages;

public interface IStorage
{
    IEnumerable<IFileSystemEntity> Entities { get; }
}
