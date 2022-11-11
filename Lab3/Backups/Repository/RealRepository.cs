using Backups.Algorithms;
using Backups.Archiver;
using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;

namespace Backups.Repository;

public class RealRepository : IRepository
{
    public RealRepository(string repPath)
    {
        RepositoryPath = repPath;
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

    public Stream CreateFile(string filePath) => File.Create(FullPath(filePath));

    public IBackupTask CreateBackupTask(IAlgorithm algorithm, IArchivator archivator)
    {
        string backupTaskPath = "BackupTask-" + Guid.NewGuid();
        Directory.CreateDirectory(FullPath(backupTaskPath));
        return new BackupTask(backupTaskPath, this, algorithm, archivator);
    }

    public string CreateRestorePointDirectory(IBackupTask backupTask)
    {
        string restorePointPath = backupTask.BackupTaskPath + Path.DirectorySeparatorChar + "RestorePoint-" + Guid.NewGuid();
        Directory.CreateDirectory(FullPath(restorePointPath));
        return restorePointPath;
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
