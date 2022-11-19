using Backups.FileSystemEntities.Interfaces;

namespace Backups.Repository;

public interface IRepository
{
    public string RepositoryPath { get; }
    public string PathSeparator { get; }
    IFileSystemEntity OpenEntity(string entityPath);
    Stream CreateFile(string filePath);
    void CreateDirectory(string dirPath);
}
