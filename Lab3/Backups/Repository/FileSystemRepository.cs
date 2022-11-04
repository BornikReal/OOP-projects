using Backups.FileSystemEntities;

namespace Backups.Repository;

public class FileSystemRepository : IRepository
{
    private readonly string _rootPath;
    public FileSystemRepository(string rootPath)
    {
        _rootPath = rootPath;
    }

    public string GetFullPath(string fileSystemPath)
    {
        return _rootPath + Path.DirectorySeparatorChar + fileSystemPath;
    }

    public bool IsDirectory(string entityPath)
    {
        return Directory.Exists(GetFullPath(entityPath));
    }

    public bool IsFile(string entityPath)
    {
        return File.Exists(GetFullPath(entityPath));
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

    private FileEntity OpenFile(string filePath)
    {
        return new FileEntity(Path.GetFileName(filePath), filePath, File.OpenRead(GetFullPath(filePath)));
    }

    private DirectoryEntity OpenDirectory(string dirPath)
    {
        var entitiesList = new List<IFileSystemEntity>();
        var dirInfo = new DirectoryInfo(GetFullPath(dirPath));
        foreach (FileInfo file in dirInfo.GetFiles())
        {
            entitiesList.Add(OpenFile(file.FullName.Replace(_rootPath, string.Empty)));
        }

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            entitiesList.Add(OpenDirectory(dir.FullName.Replace(_rootPath, string.Empty)));
        }

        return new DirectoryEntity(Path.GetFileName(dirPath), dirPath, entitiesList);
    }
}
