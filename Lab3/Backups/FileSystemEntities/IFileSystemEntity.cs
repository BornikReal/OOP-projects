namespace Backups.FileSystemEntities;

public interface IFileSystemEntity
{
    string Name { get; }
    string FullPath { get; }
}
