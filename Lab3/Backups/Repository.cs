namespace Backups;

public class Repository : IRepository
{
    public Repository() { }

    public Backup Backup { get; } = new Backup();
    public BackupTask? CurTask { get; private set; } = null;

    public void RepositoryAdd(List<BackupObject> backupObjects)
    {
        CurTask = new BackupTask(backupObjects);
    }

    public Storage RepositoryCommit()
    {
        throw new NotImplementedException();
    }
}
