using Backups.FileSystemEntities;
using Backups.Models;

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

    public FileEntity OpenFile(string filePath)
    {
        if (!IsFile(filePath))
            throw new Exception();
        return new FileEntity(Path.GetFileName(filePath), () => File.OpenRead(filePath));
    }

    public DirectoryEntity OpenDirectory(string dirPath)
    {
        if (!IsDirectory(dirPath))
            throw new Exception();
        return new DirectoryEntity(Path.GetFileName(dirPath), () => GetListEnitites(dirPath));
    }

    public IFileSystemEntity OpenEntity(string entityPath)
    {
        if (IsFile(entityPath))
            return OpenFile(entityPath);
        else if (IsDirectory(entityPath))
            return OpenDirectory(entityPath);
        throw new Exception();
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

    private IEnumerable<IFileSystemEntity> GetListEnitites(string dirPath)
    {
        var entitiesList = new List<IFileSystemEntity>();
        var dirInfo = new DirectoryInfo(dirPath);
        foreach (FileInfo file in dirInfo.GetFiles())
            entitiesList.Add(OpenFile(file.FullName));

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            entitiesList.Add(OpenDirectory(dir.FullName));

        return entitiesList;
    }
}
