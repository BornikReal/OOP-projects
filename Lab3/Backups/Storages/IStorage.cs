using Backups.FileSystemEntities.Interfaces;

namespace Backups.Storages;

public interface IStorage
{
    IEnumerable<IFileSystemEntity> GetEntities();
}
