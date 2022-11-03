namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IAlgorithm
{
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();

    public SingleStorageAlgorithm(List<BackupObject> backupObjects, string storagePath)
    {
        _backupObjects = backupObjects;
        StoragePath = storagePath;
    }

    public string StoragePath { get; }

    public void CreateBackup()
    {
    }

    public void UnpackBackup()
    {
        throw new NotImplementedException();
    }
}
