using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public interface IAlgorithm
{
    IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository);
}
