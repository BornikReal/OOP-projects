namespace Backups.FileSystemEntities.Interfaces;

public interface IDirectoryEntity : IFileSystemEntity
{
    IEnumerable<IFileSystemEntity> Entities();
}
