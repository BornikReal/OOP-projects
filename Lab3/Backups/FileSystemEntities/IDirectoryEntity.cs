namespace Backups.FileSystemEntities;

public interface IDirectoryEntity : IFileSystemEntity
{
    IEnumerable<IFileSystemEntity> Entities { get; }
}
