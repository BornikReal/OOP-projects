using Backups.FileSystemEntities;
using Backups.Models;
using Backups.Storages;
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

    public IFileSystemEntity OpenEntity(string entityPath, bool isOpenStream = true)
    {
        if (IsFile(entityPath))
            return OpenFile(entityPath, isOpenStream);
        else if (IsDirectory(entityPath))
            return OpenDirectory(entityPath, isOpenStream);
        throw new Exception();
    }

    public FileEntity OpenFile(string filePath, bool isOpenStream = true, string prevPath = "")
    {
        if (!IsFile(filePath))
            throw new Exception();
        Stream? stream = null;
        if (isOpenStream)
            stream = RepositoryFileSystem.OpenFile((UPath)filePath, FileMode.Open, FileAccess.ReadWrite);
        return new FileEntity(prevPath + RepositoryFileSystem.GetFileEntry((UPath)filePath).Name, filePath, stream);
    }

    public DirectoryEntity OpenDirectory(string dirPath, bool isOpenStream = true, string prevPath = "")
    {
        if (!IsDirectory(dirPath))
            throw new Exception();
        var entitiesList = new List<IFileSystemEntity>();
        DirectoryEntry dirInfo = RepositoryFileSystem.GetDirectoryEntry((UPath)dirPath);
        string newPrevPath = prevPath + dirInfo.Name;
        foreach (FileEntry file in dirInfo.EnumerateFiles())
        {
            entitiesList.Add(OpenFile(file.FullName, isOpenStream, newPrevPath + @"\"));
        }

        foreach (DirectoryEntry dir in dirInfo.EnumerateDirectories())
        {
            entitiesList.Add(OpenDirectory(dir.FullName, isOpenStream, newPrevPath + @"\"));
        }

        return new DirectoryEntity(newPrevPath, dirPath, entitiesList);
    }

    public void CloseEntity(IFileSystemEntity entity)
    {
        if (entity.Stream != null)
        {
            ((FileEntity)entity).Stream = null;
        }
        else
        {
            foreach (IFileSystemEntity entityPart in entity.Entities!)
                CloseEntity(entityPart);
        }
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

    public void InitStorageDirectory(Storage storage, string fullPathUnpackDirectory)
    {
        foreach (IFileSystemEntity entity in storage.Entities)
            CreateDirectorOrFile(entity, fullPathUnpackDirectory);
    }

    public void Dispose()
    {
        RepositoryFileSystem.Dispose();
        GC.SuppressFinalize(this);
    }

    private void CreateDirectorOrFile(IFileSystemEntity entity, string fullPathUnpackDirectory)
    {
        if (entity.Entities == null)
        {
            File.Create(fullPathUnpackDirectory + @"\" + entity.Name);
        }
        else
        {
            RepositoryFileSystem.CreateDirectory(fullPathUnpackDirectory + @"\" + entity.Name);
            foreach (IFileSystemEntity entityPart in entity.Entities!)
                CreateDirectorOrFile(entityPart, fullPathUnpackDirectory);
        }
    }
}
