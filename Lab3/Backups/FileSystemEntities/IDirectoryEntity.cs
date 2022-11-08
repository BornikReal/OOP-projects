namespace Backups.FileSystemEntities;

public interface IDirectoryEntity : IFileSystemEntity
{
    Func<IEnumerable<IFileSystemEntity>> Entities { get; }
}
