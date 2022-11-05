using Backups.FileSystemEntities;

namespace Backups.Repository;

public interface IRepository
{
    public string RepositoryPath { get; }
    bool IsDirectory(string entityPath);
    bool IsFile(string entityPath);
    FileEntity OpenFile(string filePath, bool isOpenStream = true, string prevPath = "");
    DirectoryEntity OpenDirectory(string dirPath, bool isOpenStream = true, string prevPath = "");
    IFileSystemEntity OpenEntity(string entityPath, bool isOpenStream = true);
    void CloseEntity(IFileSystemEntity entity);
    string CreateBackupTaskDirectory(BackupTask backupTask);
    string CreateRestorePointDirectory(RestorePoint restorePoint);
    void InitStorageDirectory(Storage storage, string fullPathUnpackDirectory);
}
