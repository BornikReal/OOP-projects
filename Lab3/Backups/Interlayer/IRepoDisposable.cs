using Backups.FileSystemEntities.Interfaces;

namespace Backups.Interlayer;

public interface IRepoDisposable : IDisposable
{
    IEnumerable<IFileSystemEntity> Entities { get; }
}
