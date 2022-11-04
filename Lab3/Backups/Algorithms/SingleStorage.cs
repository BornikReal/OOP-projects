using Backups.Repository;

namespace Backups.Algorithms;

public class SingleStorage : IAlgorithm
{
    private readonly List<BackupObject> _backupObjects = new List<BackupObject>();
    private readonly Archivator _archivator;

    public SingleStorage(List<BackupObject> backupObjects, Archivator archivator, Guid restorId)
    {
        _backupObjects = backupObjects;
        _archivator = archivator;
        RestoreId = restorId;
    }

    public Guid RestoreId { get; }

    public void CreateBackup()
    {
        foreach (BackupObject backupObject in _backupObjects)
        {
            _archivator.AddEntryToArchive(backupObject.ObjectPath);
        }

        _archivator.MakeArchive(_archivator.BackupsFolder + Path.DirectorySeparatorChar + RestoreId);
    }

    public void UnpackBackup(string unpackFolder)
    {
        _archivator.Unarchive(_archivator.BackupsFolder + Path.DirectorySeparatorChar + RestoreId, unpackFolder);
    }
}
