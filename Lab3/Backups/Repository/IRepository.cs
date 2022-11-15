using Backups.Algorithms;
using Backups.Archiver;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;

namespace Backups.Repository;

public interface IRepository
{
    public string RepositoryPath { get; }
    public string PathSeparator { get; }
    bool IsDirectory(string entityPath);
    bool IsFile(string entityPath);
    IFileEntity OpenFile(string filePath);
    IDirectoryEntity OpenDirectory(string dirPath);
    IFileSystemEntity OpenEntity(string entityPath);
    Stream CreateFile(string filePath);
    void CreateDirectory(string dirPath);
    IBackupTask CreateBackupTask(IAlgorithm algorithm, IArchivator archivator);
    string CreateRestorePointDirectory(IBackupTask backupTask);
}
