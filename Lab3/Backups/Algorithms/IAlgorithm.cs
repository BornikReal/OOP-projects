using Backups.Repository;

namespace Backups.Algorithms;

public interface IAlgorithm
{
    List<Storage> CreateBackup(List<BackupObject> backupObjects, IArchivator archivator, string restorPointPath);
    void UnpackBackup(IArchivator archivator, List<Storage> storages, string unpackFolder);
}
