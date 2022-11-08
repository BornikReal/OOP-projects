namespace Backups.FileSystemEntities.Interfaces;

public interface IDirectoryEntity : IFileSystemEntity
{
    Func<IEnumerable<IFileSystemEntity>> Entities { get; }
}
