using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;

namespace Backups.Extra.RepositorySuper;

public class RealRepositorySuper : IRepositorySuper
{
    private readonly RealRepository _realRepository;
    public RealRepositorySuper(string repositoryPath)
    {
        _realRepository = new RealRepository(repositoryPath);
    }

    public string RepositoryPath => _realRepository.RepositoryPath;

    public string PathSeparator => _realRepository.PathSeparator;

    public void CreateDirectory(string dirPath)
    {
        _realRepository.CreateDirectory(dirPath);
    }

    public Stream CreateFile(string filePath)
    {
        return _realRepository.CreateFile(filePath);
    }

    public bool IsDirectory(string entityPath)
    {
        return _realRepository.IsDirectory(entityPath);
    }

    public bool IsFile(string entityPath)
    {
        return _realRepository.IsFile(entityPath);
    }

    public IDirectoryEntity OpenDirectory(string dirPath)
    {
        return _realRepository.OpenDirectory(dirPath);
    }

    public IFileSystemEntity OpenEntity(string entityPath)
    {
        return _realRepository.OpenEntity(entityPath);
    }

    public IFileEntity OpenFile(string filePath)
    {
        return _realRepository.OpenFile(filePath);
    }

    public void DeleteEntity(string path)
    {
        if (IsFile(path))
            File.Delete(_realRepository.FullPath(path));
        else if (IsDirectory(path))
            Directory.Delete(_realRepository.FullPath(path), true);
        else
            throw new Exception();
    }
}
