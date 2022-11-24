using Backups.Exceptions;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Zio;

namespace Backups.Extra.RepositorySuper;

public class InMemoryRepositorySuper : IRepositorySuper, IDisposable
{
    private readonly InMemoryRepository _inMemoryRepository;
    public InMemoryRepositorySuper(string repositoryPath)
    {
        _inMemoryRepository = new InMemoryRepository(repositoryPath);
    }

    public string RepositoryPath => _inMemoryRepository.RepositoryPath;

    public string PathSeparator => _inMemoryRepository.PathSeparator;

    public void CreateDirectory(string dirPath)
    {
        _inMemoryRepository.CreateDirectory(dirPath);
    }

    public Stream CreateFile(string filePath)
    {
        return _inMemoryRepository.CreateFile(filePath);
    }

    public bool IsDirectory(string entityPath)
    {
        return _inMemoryRepository.IsDirectory(entityPath);
    }

    public bool IsFile(string entityPath)
    {
        return _inMemoryRepository.IsFile(entityPath);
    }

    public IDirectoryEntity OpenDirectory(string dirPath)
    {
        return _inMemoryRepository.OpenDirectory(dirPath);
    }

    public IFileSystemEntity OpenEntity(string entityPath)
    {
        return _inMemoryRepository.OpenEntity(entityPath);
    }

    public IFileEntity OpenFile(string filePath)
    {
        return _inMemoryRepository.OpenFile(filePath);
    }

    public void DeleteEntity(string path)
    {
        if (IsFile(path))
            _inMemoryRepository.RepositoryFileSystem.DeleteFile((UPath)_inMemoryRepository.FullPath(path));
        else if (IsDirectory(path))
            _inMemoryRepository.RepositoryFileSystem.DeleteDirectory((UPath)_inMemoryRepository.FullPath(path), true);
        else
            throw new RepositoryOpenException();
    }

    public void Dispose()
    {
        _inMemoryRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
