using Backups.Exceptions;
using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;

namespace Backups.Repository;

public class RealRepository : IRepository
{
    public RealRepository(string repositoryPath)
    {
        RepositoryPath = repositoryPath;
    }

    public string RepositoryPath { get; }
    public string PathSeparator { get; } = Path.DirectorySeparatorChar.ToString();
    public string FullPath(string enityPath) => Path.Combine(RepositoryPath, enityPath);

    public bool IsDirectory(string entityPath)
    {
        return Directory.Exists(FullPath(entityPath));
    }

    public bool IsFile(string entityPath)
    {
        return File.Exists(FullPath(entityPath));
    }

    public IFileEntity OpenFile(string filePath)
    {
        if (!IsFile(filePath))
            throw new RepositoryOpenException();
        return new FileEntity(Path.GetFileName(FullPath(filePath)), () => File.OpenRead(FullPath(filePath)));
    }

    public IDirectoryEntity OpenDirectory(string dirPath)
    {
        if (!IsDirectory(dirPath))
            throw new RepositoryOpenException();
        return new DirectoryEntity(Path.GetFileName(FullPath(dirPath)), () => GetListEnitites(FullPath(dirPath)));
    }

    public IFileSystemEntity OpenEntity(string entityPath)
    {
        if (IsFile(entityPath))
            return OpenFile(entityPath);
        else if (IsDirectory(entityPath))
            return OpenDirectory(entityPath);
        throw new RepositoryOpenException();
    }

    public Stream CreateFile(string filePath) => File.Create(FullPath(filePath));
    public void CreateDirectory(string dirPath) => Directory.CreateDirectory(FullPath(dirPath));

    private IEnumerable<IFileSystemEntity> GetListEnitites(string dirPath)
    {
        var entitiesList = new List<IFileSystemEntity>();
        var dirInfo = new DirectoryInfo(FullPath(dirPath));
        foreach (FileInfo file in dirInfo.GetFiles())
            entitiesList.Add(OpenFile(dirPath + @"\" + file.Name));

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            entitiesList.Add(OpenDirectory(dirPath + @"\" + dir.Name));

        return entitiesList;
    }
}
