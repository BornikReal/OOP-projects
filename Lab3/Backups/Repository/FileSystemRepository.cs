using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;

namespace Backups.Repository;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string repPath)
    {
        RepositoryPath = repPath;
    }

    public string RepositoryPath { get; }
    public string FullPath(string enityPath) => Path.Combine(RepositoryPath, enityPath);

    public bool IsDirectory(string entityPath)
    {
        return Directory.Exists(FullPath(entityPath));
    }

    public bool IsFile(string entityPath)
    {
        return File.Exists(FullPath(entityPath));
    }

    public FileEntity OpenFile(string filePath)
    {
        if (!IsFile(filePath))
            throw new Exception();
        return new FileEntity(Path.GetFileName(FullPath(filePath)), () => File.OpenRead(FullPath(filePath)));
    }

    public DirectoryEntity OpenDirectory(string dirPath)
    {
        if (!IsDirectory(dirPath))
            throw new Exception();
        return new DirectoryEntity(Path.GetFileName(FullPath(dirPath)), () => GetListEnitites(FullPath(dirPath)));
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
        string backupTaskDirectory = FullPath("BackupTask-" + backupTask.Id);
        if (IsDirectory(backupTaskDirectory))
            throw new Exception();
        Directory.CreateDirectory(FullPath(backupTaskDirectory));
        return backupTaskDirectory;
    }

    public string CreateRestorePointDirectory(RestorePoint restorePoint)
    {
        string restorePointDirectory = FullPath("RestorePoint-" + restorePoint.Id);
        if (IsDirectory(restorePointDirectory))
            throw new Exception();
        Directory.CreateDirectory(FullPath(restorePointDirectory));
        return restorePointDirectory;
    }

    private IEnumerable<IFileSystemEntity> GetListEnitites(string dirPath)
    {
        var entitiesList = new List<IFileSystemEntity>();
        var dirInfo = new DirectoryInfo(FullPath(dirPath));
        foreach (FileInfo file in dirInfo.GetFiles())
            entitiesList.Add(OpenFile(file.FullName.Replace(RepositoryPath + Path.DirectorySeparatorChar, string.Empty)));

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            entitiesList.Add(OpenDirectory(dir.FullName.Replace(RepositoryPath + Path.DirectorySeparatorChar, string.Empty)));

        return entitiesList;
    }
}
