using Backups.FileSystemEntities;

namespace Backups.Repository;

public class FileSystemRepository : IRepository
{
    private readonly string _rootPath;
    public FileSystemRepository(string rootPath)
    {
        _rootPath = rootPath;
    }

    public static bool IsDirectory(string entityPath)
    {
        return Directory.Exists(entityPath);
    }

    public static bool IsFile(string entityPath)
    {
        return File.Exists(entityPath);
    }

    public FileEntity OpenFile(string filePath)
    {
        if (!IsFile(filePath))
            throw new Exception();
        return new FileEntity(Path.GetFileName(filePath), filePath, File.Open(filePath, FileMode.Open, FileAccess.ReadWrite));
    }

    public DirectoryEntity OpenDirectory(string dirPath)
    {
        if (!IsDirectory(dirPath))
            throw new Exception();
        var entitiesList = new List<IFileSystemEntity>();
        var dirInfo = new DirectoryInfo(dirPath);
        foreach (FileInfo file in dirInfo.GetFiles())
        {
            entitiesList.Add(OpenFile(file.FullName));
        }

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            entitiesList.Add(OpenDirectory(dir.FullName));
        }

        return new DirectoryEntity(Path.GetFileName(dirPath), dirPath, entitiesList);
    }

    public IFileSystemEntity OpenEntity(string entityPath)
    {
        if (IsFile(entityPath))
            return OpenFile(entityPath);
        else if (IsDirectory(entityPath))
            return OpenDirectory(entityPath);
        throw new Exception();
    }

    public void CloseEntity(IFileSystemEntity entity)
    {
        if (entity.Stream != null)
        {
            entity.Stream.Close();
        }
        else
        {
            foreach (IFileSystemEntity entityPart in entity.Entities!)
                CloseEntity(entityPart);
        }
    }

    public string CreateBackupTaskDirectory(BackupTask backupTask)
    {
        string backupTaskDirectory = _rootPath + Path.DirectorySeparatorChar + "BackupTask-" + backupTask.Id;
        if (IsDirectory(backupTaskDirectory))
            throw new Exception();
        Directory.CreateDirectory(backupTaskDirectory);
        return backupTaskDirectory;
    }

    public string CreateRestorePointDirectory(RestorePoint restorePoint)
    {
        string restorePointDirectory = _rootPath + Path.DirectorySeparatorChar + "RestorePoint-" + restorePoint.Id;
        if (IsDirectory(restorePointDirectory))
            throw new Exception();
        Directory.CreateDirectory(restorePointDirectory);
        return restorePointDirectory;
    }
}
