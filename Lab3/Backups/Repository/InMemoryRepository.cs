using Backups.Algorithms;
using Backups.Archiver;
using Backups.Exceptions;
using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Repository;

public class InMemoryRepository : IRepository, IDisposable
{
    public InMemoryRepository(string repositoryPath)
    {
        RepositoryPath = $@"\mnt\c\{repositoryPath}";
        RepositoryFileSystem = new MemoryFileSystem();
        RepositoryFileSystem.CreateDirectory((UPath)RepositoryPath);
    }

    public string RepositoryPath { get; }
    public string PathSeparator { get; } = @"\";
    public MemoryFileSystem RepositoryFileSystem { get; }
    public string FullPath(string enityPath) => RepositoryPath + @"\" + enityPath;
    public Stream CreateFile(string filePath) => RepositoryFileSystem.CreateFile((UPath)FullPath(filePath));
    public void CreateDirectory(string dirPath) => RepositoryFileSystem.CreateDirectory((UPath)FullPath(dirPath));

    public bool IsDirectory(string entityPath)
    {
        return RepositoryFileSystem.DirectoryExists((UPath)FullPath(entityPath));
    }

    public bool IsFile(string entityPath)
    {
        return RepositoryFileSystem.FileExists((UPath)FullPath(entityPath));
    }

    public IBackupTask CreateBackupTask(IAlgorithm algorithm, IArchivator archivator)
    {
        string backupTaskPath = "BackupTask-" + Guid.NewGuid();
        CreateDirectory(backupTaskPath);
        return new BackupTask(backupTaskPath, this, algorithm, archivator);
    }

    public string CreateRestorePointDirectory(IBackupTask backupTask)
    {
        string restorePointPath = backupTask.BackupTaskPath + Path.DirectorySeparatorChar + "RestorePoint-" + Guid.NewGuid();
        CreateDirectory(restorePointPath);
        return restorePointPath;
    }

    public IFileSystemEntity OpenEntity(string entityPath)
    {
        if (IsFile(entityPath))
            return OpenFile(entityPath);
        else if (IsDirectory(entityPath))
            return OpenDirectory(entityPath);
        throw new RepositoryOpenException();
    }

    public IFileEntity OpenFile(string filePath)
    {
        if (!IsFile(filePath))
            throw new RepositoryOpenException();
        return new FileEntity(RepositoryFileSystem.GetFileEntry((UPath)FullPath(filePath)).Name, () => RepositoryFileSystem.OpenFile((UPath)FullPath(filePath), FileMode.Open, FileAccess.Read));
    }

    public IDirectoryEntity OpenDirectory(string dirPath)
    {
        if (!IsDirectory(dirPath))
            throw new RepositoryOpenException();
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
            entitiesList.Add(OpenFile(dirPath + @"\" + file.Name));
        }

        foreach (DirectoryEntry dir in dirInfo.EnumerateDirectories())
        {
            entitiesList.Add(OpenDirectory(dirPath + @"\" + dir.Name));
        }

        return entitiesList;
    }
}
