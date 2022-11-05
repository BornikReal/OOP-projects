namespace Backups.FileSystemEntities;

public interface IFileEntity : IFileSystemEntity
{
    Stream? Stream { get; }
}
