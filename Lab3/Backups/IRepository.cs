namespace Backups;

public interface IRepository
{
    void RepositoryAdd(List<BackupObject> backupObjects);
    Storage RepositoryCommit();
}
