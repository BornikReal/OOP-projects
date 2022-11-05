using Backups.FileSystemEntities;
using Backups.Models;
using Backups.Storages;

namespace Backups.Repository;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string repPath)
    {
        RepositoryPath = repPath;
    }

    public string RepositoryPath { get; }

    public bool IsDirectory(string entityPath)
    {
        return Directory.Exists(entityPath);
    }

    public bool IsFile(string entityPath)
    {
        return File.Exists(entityPath);
    }

    public FileEntity OpenFile(string filePath, bool isOpenStream = true, string prevPath = "")
    {
        if (!IsFile(filePath))
            throw new Exception();
        Stream? stream = null;
        if (isOpenStream)
            stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
        return new FileEntity(prevPath + Path.DirectorySeparatorChar + Path.GetFileName(filePath), filePath, stream);
    }

    public DirectoryEntity OpenDirectory(string dirPath, bool isOpenStream = true, string prevPath = "")
    {
        if (!IsDirectory(dirPath))
            throw new Exception();
        var entitiesList = new List<IFileSystemEntity>();
        var dirInfo = new DirectoryInfo(dirPath);
        string newPrevPath = prevPath + Path.DirectorySeparatorChar + dirInfo.Name;
        foreach (FileInfo file in dirInfo.GetFiles())
        {
            entitiesList.Add(OpenFile(file.FullName, isOpenStream, newPrevPath));
        }

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            entitiesList.Add(OpenDirectory(dir.FullName, isOpenStream, newPrevPath));
        }

        return new DirectoryEntity(prevPath + Path.DirectorySeparatorChar + Path.GetFileName(dirPath), dirPath, entitiesList);
    }

    public IFileSystemEntity OpenEntity(string entityPath, bool isOpenStream = true)
    {
        if (IsFile(entityPath))
            return OpenFile(entityPath, isOpenStream);
        else if (IsDirectory(entityPath))
            return OpenDirectory(entityPath, isOpenStream);
        throw new Exception();
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
        string backupTaskDirectory = RepositoryPath + Path.DirectorySeparatorChar + "BackupTask-" + backupTask.Id;
        if (IsDirectory(backupTaskDirectory))
            throw new Exception();
        Directory.CreateDirectory(backupTaskDirectory);
        return backupTaskDirectory;
    }

    public string CreateRestorePointDirectory(RestorePoint restorePoint)
    {
        string restorePointDirectory = RepositoryPath + Path.DirectorySeparatorChar + "RestorePoint-" + restorePoint.Id;
        if (IsDirectory(restorePointDirectory))
            throw new Exception();
        Directory.CreateDirectory(restorePointDirectory);
        return restorePointDirectory;
    }

    public void InitStorageDirectory(Storage storage, string fullPathUnpackDirectory)
    {
        foreach (IFileSystemEntity entity in storage.Entities)
            CreateDirectorOrFile(entity, fullPathUnpackDirectory);
    }

    private void CreateDirectorOrFile(IFileSystemEntity entity, string fullPathUnpackDirectory)
    {
        if (entity.Entities == null)
        {
            ((FileEntity)entity).Stream = File.Open(fullPathUnpackDirectory + Path.DirectorySeparatorChar + entity.Name, FileMode.Open, FileAccess.ReadWrite);
        }
        else
        {
            Directory.CreateDirectory(fullPathUnpackDirectory + Path.DirectorySeparatorChar + entity.Name);
            foreach (IFileSystemEntity entityPart in entity.Entities!)
                CreateDirectorOrFile(entityPart, fullPathUnpackDirectory);
        }
    }
}
