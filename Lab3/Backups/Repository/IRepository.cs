using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;

namespace Backups.Repository;

public interface IRepository
{
    public string RepositoryPath { get; }
    bool IsDirectory(string entityPath);
    bool IsFile(string entityPath);
    FileEntity OpenFile(string filePath);
    DirectoryEntity OpenDirectory(string dirPath);
    IFileSystemEntity OpenEntity(string entityPath);
    string CreateBackupTaskDirectory(BackupTask backupTask);
    string CreateRestorePointDirectory(RestorePoint restorePoint);
}
