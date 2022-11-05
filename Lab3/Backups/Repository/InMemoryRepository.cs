using Backups.FileSystemEntities;
using Backups.Models;
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

    public bool IsDirectory(string entityPath)
    {
        return RepositoryFileSystem.FileExists((UPath)entityPath);
    }

    public bool IsFile(string entityPath)
    {
        return RepositoryFileSystem.DirectoryExists((UPath)entityPath);
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
        return new FileEntity(RepositoryFileSystem.GetFileEntry((UPath)filePath).Name, filePath, () => RepositoryFileSystem.OpenFile((UPath)filePath, FileMode.Open, FileAccess.Read));
    }

    public DirectoryEntity OpenDirectory(string dirPath)
    {
        if (!IsDirectory(dirPath))
            throw new Exception();
        var entitiesList = new List<IFileSystemEntity>();
        DirectoryEntry dirInfo = RepositoryFileSystem.GetDirectoryEntry((UPath)dirPath);
        foreach (FileEntry file in dirInfo.EnumerateFiles())
        {
            entitiesList.Add(OpenFile(file.FullName));
        }

        foreach (DirectoryEntry dir in dirInfo.EnumerateDirectories())
        {
            entitiesList.Add(OpenDirectory(dir.FullName));
        }

        return new DirectoryEntity(dirInfo.Name, dirPath, entitiesList);
    }

    public string CreateBackupTaskDirectory(BackupTask backupTask)
    {
        string backupTaskDirectory = RepositoryPath + @"\" + "BackupTask-" + backupTask.Id;
        if (IsDirectory(backupTaskDirectory))
            throw new Exception();
        RepositoryFileSystem.CreateDirectory((UPath)backupTaskDirectory);
        return backupTaskDirectory;
    }

    public string CreateRestorePointDirectory(RestorePoint restorePoint)
    {
        string restorePointDirectory = RepositoryPath + @"\" + "RestorePoint-" + restorePoint.Id;
        if (IsDirectory(restorePointDirectory))
            throw new Exception();
        RepositoryFileSystem.CreateDirectory((UPath)restorePointDirectory);
        return restorePointDirectory;
    }

    public void Dispose()
    {
        RepositoryFileSystem.Dispose();
        GC.SuppressFinalize(this);
    }
}
