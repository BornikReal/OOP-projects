namespace Backups.FileSystemEntities.Interfaces;

public interface IFileEntity : IFileSystemEntity
{
    Stream FuncStream();
}
