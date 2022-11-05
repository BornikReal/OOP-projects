using Backups.Archivator;
using Backups.Repository;

namespace Backups.Algorithms;

public interface IAlgorithm
{
    List<Storage> CreateBackup(List<BackupObject> backupObjects, string restorPointPath, IRepository repository, IArchivator archivator);
}
