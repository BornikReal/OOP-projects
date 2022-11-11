using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;

namespace Backups.Repository;

public interface IRepository
{
    public string RepositoryPath { get; }
    public string PathSeparator { get; }
    bool IsDirectory(string entityPath);
    bool IsFile(string entityPath);
    FileEntity OpenFile(string filePath);
    DirectoryEntity OpenDirectory(string dirPath);
    IFileSystemEntity OpenEntity(string entityPath);
    Stream CreateFile(string filePath);
}
