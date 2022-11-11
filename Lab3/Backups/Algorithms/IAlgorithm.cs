using Backups.Archiver;
using Backups.Models;
using Backups.Repository;
using Backups.Storages;

namespace Backups.Algorithms;

public interface IAlgorithm
{
    IStorage CreateBackup(List<BackupObject> backupObjects, string restorPointPath, IRepository repository, IArchivator archiver);
}
