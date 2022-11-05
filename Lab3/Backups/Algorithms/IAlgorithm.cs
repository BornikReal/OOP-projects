using Backups.Archivator;
using Backups.Models;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public interface IAlgorithm
{
    List<Storage> CreateBackup(List<BackupObject> backupObjects, string restorPointPath, IRepository repository, IArchivator archivator);
}
