namespace Backups.FileSystemEntities.Interfaces;

public interface IFileEntity : IFileSystemEntity
{
    Func<Stream> FuncStream { get; }
}
