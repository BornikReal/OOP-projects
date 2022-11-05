namespace Backups.FileSystemEntities;

public interface IFileEntity : IFileSystemEntity
{
    Func<Stream> FuncStream { get; }
}
