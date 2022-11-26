using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;

namespace Backups.Extra.RepositorySuper;

public interface IRepositorySuper : IRepository
{
    void DeleteEntity(string path);
    bool IsDirectory(string entityPath);
    bool IsFile(string entityPath);
    IFileEntity OpenFile(string filePath);
    IDirectoryEntity OpenDirectory(string dirPath);
}
