using Backups.Extra.LoggingEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Extra.AlgorithmSuper;

public interface IAlgorithmSuper
{
    IStorage CreateBackup(IEnumerable<IFileSystemEntity> entities, string restorPointPath, IRepository repository, ILogger logger);
}
