using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;
using Zio;
using Zio.FileSystems;

namespace Backups.Repository;

public class InMemoryRepository : IRepository, IDisposable
{
    public InMemoryRepository(string repPath)
    {
        RepositoryPath = $@"\mnt\c\{repPath}";
        RepositoryFileSystem = new MemoryFileSystem();
        RepositoryFileSystem.CreateDirectory((UPath)RepositoryPath);
    }

    public string RepositoryPath { get; }
    public MemoryFileSystem RepositoryFileSystem { get; }
    public string FullPath(string enityPath) => RepositoryPath + @"\" + enityPath;
    public Stream CreateFile(string filePath) => RepositoryFileSystem.OpenFile((UPath)FullPath(filePath), FileMode.Create, FileAccess.Write);

    public bool IsDirectory(string entityPath)
    {
        return RepositoryFileSystem.FileExists((UPath)FullPath(entityPath));
    }

    public bool IsFile(string entityPath)
    {
        return RepositoryFileSystem.DirectoryExists((UPath)FullPath(entityPath));
    }

    public IFileSystemEntity OpenEntity(string entityPath)
    {
        if (IsFile(entityPath))
            return OpenFile(entityPath);
        else if (IsDirectory(entityPath))
            return OpenDirectory(entityPath);
        throw new Exception();
    }

    public FileEntity OpenFile(string filePath)
    {
        if (!IsFile(filePath))
            throw new Exception();
        return new FileEntity(RepositoryFileSystem.GetFileEntry((UPath)FullPath(filePath)).Name, () => RepositoryFileSystem.OpenFile((UPath)FullPath(filePath), FileMode.Open, FileAccess.Read));
    }

    public DirectoryEntity OpenDirectory(string dirPath)
    {
        if (!IsDirectory(dirPath))
            throw new Exception();
        DirectoryEntry dirInfo = RepositoryFileSystem.GetDirectoryEntry((UPath)FullPath(dirPath));
        return new DirectoryEntity(dirInfo.Name, () => GetListEnitites(dirPath));
    }

    public void Dispose()
    {
        RepositoryFileSystem.Dispose();
        GC.SuppressFinalize(this);
    }

    private IEnumerable<IFileSystemEntity> GetListEnitites(string dirPath)
    {
        var entitiesList = new List<IFileSystemEntity>();
        DirectoryEntry dirInfo = RepositoryFileSystem.GetDirectoryEntry((UPath)FullPath(dirPath));
        foreach (FileEntry file in dirInfo.EnumerateFiles())
        {
            entitiesList.Add(OpenFile(file.FullName.Replace(RepositoryPath + @"\", string.Empty)));
        }

        foreach (DirectoryEntry dir in dirInfo.EnumerateDirectories())
        {
            entitiesList.Add(OpenDirectory(dir.FullName.Replace(RepositoryPath + @"\", string.Empty)));
        }

        return entitiesList;
    }
}
